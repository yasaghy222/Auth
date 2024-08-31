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

        public async Task<ErrorOr<TokenResponse>> Handle(
            LoginCommand command, CancellationToken ct)
        {
            Option<OrganizationInfo> getOrganization = await _organizationRepository
                .GetInfoAsync(command.OrganizationId, ct);

            if (getOrganization.IsNone)
            {
                return UserErrors.OrganizationNotFound();
            }

            OrganizationInfo organization = getOrganization.ValueUnsafe();

            ILoginHandler loginHandler = command.Type switch
            {
                UserLoginType.Password => _serviceProvider
                    .GetRequiredService<LoginByPasswordHandler>(),
                _ => throw new ArgumentNullException(nameof(command)),
            };

            ErrorOr<User> loginResponse = await loginHandler
                .HandleLogin(command.MapToHandlerCommand(),
                organization.ChidesIds, ct);

            if (loginResponse.IsError)
            {
                return loginResponse.Errors;
            }

            User user = loginResponse.Value;


            GenerateTokenRequest generateTokenRequest = new()
            {
                UserInfo = user.MapToInfo(),
                SessionId = Ulid.NewUlid(DateTime.UtcNow),
                LoginOrganizationTitle = organization.Title,
                LoginOrganizationId = organization.Id,
                UserOrganizations = user.UserOrganizations.MapToInfo(),
            };

            TokenResponse tokenResponse = _tokenService
                .GenerateTokens(generateTokenRequest);


            CreateSessionRequest createSessionRequest = new()
            {
                Id = generateTokenRequest.SessionId,
                IP = command.IP,
                OrganizationId = organization.Id,
                OrganizationTitle = organization.Title,
                UniqueId = command.UniqueId,
                Platform = command.Platform,
                ExpireAt = tokenResponse.RefreshTokenExpiry,
                UserId = user.Id,
            };

            await _sessionService.CreateAsync(createSessionRequest);

            return tokenResponse;
        }
    }
}