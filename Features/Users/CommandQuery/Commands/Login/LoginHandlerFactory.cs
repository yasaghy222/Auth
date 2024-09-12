using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Services;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Organizations.Repositories;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Organizations.Contracts.Responses;
using Auth.Features.UserOrganizations.Contracts.Mappings;
using Auth.Shared.Extensions;

namespace Auth.Features.Users.CommandQuery.Commands.Login
{
    public interface ILoginHandler
    {
        Task<ErrorOr<User>> HandleLogin(
            LoginHandlerCommand command,
            IEnumerable<Ulid>? organizationChildsIds,
            CancellationToken ct);
    }

    public class LoginHandlerFactory(
        ITokenService tokenService,
        ISessionService sessionService,
        IServiceProvider serviceProvider,
        IOrganizationRepository organizationRepository)
        : IRequestHandler<LoginCommand,
         ErrorOr<TokenResponse>>
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly ISessionService _sessionService = sessionService;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IOrganizationRepository _organizationRepository = organizationRepository;

        private async Task<ErrorOr<OrganizationInfo>> GetOrganization(
            Ulid organizationId, CancellationToken ct)
        {
            Option<OrganizationInfo> getOrganization = await
                _organizationRepository.GetInfoAsync(organizationId, ct);

            if (getOrganization.IsNone)
            {
                return UserErrors.OrganizationNotFound();
            }

            return getOrganization.ValueUnsafe();
        }

        private static ErrorOr<bool> IsInOrganizations(
            OrganizationInfo organizationInfo, User user)
        {
            if (user.UserOrganizations.Any(i =>
                organizationInfo.ChidesIds.Contains(i.OrganizationId)))
            {
                return true;
            }

            return UserErrors.NotFound();
        }


        private TokenResponse GenerateToken(
            Ulid sessionId,
            User user,
            OrganizationInfo organizationInfo,
            IEnumerable<Ulid> permissions)
        {
            GenerateTokenRequest generateTokenRequest = new()
            {
                UserInfo = user.MapToInfo(),
                SessionId = sessionId,
                LoginOrganizationTitle = organizationInfo.Title,
                LoginOrganizationId = organizationInfo.Id,
                UserOrganizations = user.UserOrganizations.MapToInfo(),
                Permissions = permissions
            };

            return _tokenService.GenerateTokens(generateTokenRequest);
        }

        private async Task<ErrorOr<User>> LoginHandler(
            LoginCommand command,
            OrganizationInfo organizationInfo,
            CancellationToken ct)
        {
            ILoginHandler loginHandler = command.Type switch
            {
                UserLoginType.Password => _serviceProvider
                    .GetRequiredService<LoginByPasswordHandler>(),
                _ => throw new ArgumentNullException(nameof(command)),
            };

            return await loginHandler
                .HandleLogin(command.MapToHandlerCommand(),
                organizationInfo.ChidesIds, ct);
        }

        private async Task SubmitSession(SubmitSessionRequest request)
        {
            await _sessionService.DeleteSessionAsync(
                request.UserId,
                i => i.Platform == request.Platform
                    && i.IP == request.IP
                    && i.OrganizationId == request.OrganizationId);

            CreateSessionRequest createSessionRequest = request.MapToCreateRequest();
            await _sessionService.CreateAsync(createSessionRequest);
        }

        public async Task<ErrorOr<TokenResponse>> Handle(
            LoginCommand command, CancellationToken ct)
        {
            ErrorOr<OrganizationInfo> getOrganization = await
                GetOrganization(command.OrganizationId, ct);

            if (getOrganization.IsError)
            {
                return getOrganization.Errors;
            }

            OrganizationInfo organizationInfo = getOrganization.Value;

            ErrorOr<User> loginResponse = await LoginHandler(command, organizationInfo, ct);
            if (loginResponse.IsError)
            {
                return loginResponse.Errors;
            }

            User user = loginResponse.Value;

            ErrorOr<bool> isInOrganizations = IsInOrganizations(organizationInfo, user);
            if (isInOrganizations.IsError)
            {
                return isInOrganizations.Errors;
            }

            Ulid sessionId = Ulid.NewUlid(DateTime.UtcNow);

            IEnumerable<Ulid> permissions = user.UserOrganizations
                .SelectMany(x => x.Role?.Permissions.Select(i => i.Id) ?? []);

            TokenResponse tokenResponse = GenerateToken(
                sessionId, user, organizationInfo, permissions);

            SubmitSessionRequest submitSessionRequest = new()
            {
                SessionId = sessionId,
                IP = command.IP,
                ExpireAt = tokenResponse.RefreshTokenExpiry,
                OrganizationId = organizationInfo.Id,
                OrganizationTitle = organizationInfo.Title,
                Platform = command.Platform,
                UniqueId = command.UniqueId,
                UserId = user.Id
            };
            await SubmitSession(submitSessionRequest);

            return tokenResponse;
        }
    }
}