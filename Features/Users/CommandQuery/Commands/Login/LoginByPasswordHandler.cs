using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Events.LoginFailed;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Events.ResetFailedStatus;

namespace Auth.Features.Users.CommandQuery.Commands.Login
{
    public class LoginByPasswordHandler(
        IOrganizationRepository userRepository,
        IHashService hashService,
        IMediator mediator) :
        ILoginHandler
    {
        private readonly IOrganizationRepository _userRepository = userRepository;
        private readonly IHashService _hashService = hashService;
        private readonly IMediator _mediator = mediator;

        private async Task<ErrorOr<User>> GetUser(
            string username,
            IEnumerable<Ulid>? organizationChidesIds,
            CancellationToken ct)
        {
            Option<User> getUser = await _userRepository
                          .FindAsync(i => i.Username == username,
                          organizationChidesIds, ct);

            if (getUser.IsNone)
            {
                return UserErrors.NotFound();
            }

            return getUser.ValueUnsafe();
        }

        private async Task ResetFailedStatusEvent(
            User user, CancellationToken ct)
        {
            ResetFailedStatusEvent resetFailedStatusEvent = user.MapToEvent();
            await _mediator.Publish(resetFailedStatusEvent, ct);
        }

        private async Task<ErrorOr<bool>> CheckBlockStatus(
            User user, CancellationToken ct)
        {
            if (user.AccountLockedUntil > DateTime.UtcNow)
            {
                return UserErrors.SuspendStatus();
            }
            else if (user.AccountLockedUntil <= DateTime.UtcNow
                && user.Status == UserStatus.Blocked)
            {
                await ResetFailedStatusEvent(user, ct);
            }

            return true;
        }


        private async Task LoginFailedEvent(
            User user, string reason, CancellationToken ct)
        {
            LoginFailedEvent loginFailedEvent = user.MapToEvent(reason);

            await _mediator.Publish(loginFailedEvent, ct);
        }

        private async Task<ErrorOr<bool>> CheckActiveStatus(
            User user, CancellationToken ct)
        {
            if (user.Status != UserStatus.Active)
            {
                await LoginFailedEvent(user, "User account is suspended", ct);
                return UserErrors.SuspendStatus();
            }
            return true;
        }

        private async Task<ErrorOr<bool>> CheckPassword(
            User user, string password, CancellationToken ct)
        {
            if (_hashService.VerifyHashString(
                user.Password, password))
            {
                return true;
            }

            await LoginFailedEvent(user, "Invalid password", ct);
            return UserErrors.InvalidUserPass();
        }


        public async Task<ErrorOr<User>> HandleLogin(
               LoginHandlerCommand command,
               IEnumerable<Ulid>? organizationChidesIds,
               CancellationToken ct)
        {
            ErrorOr<User> getUser = await GetUser(command.Username, organizationChidesIds, ct);
            if (getUser.IsError)
            {
                return getUser.Errors;
            }

            User user = getUser.Value;

            ErrorOr<bool> checkBlockStatus = await CheckBlockStatus(user, ct);
            if (checkBlockStatus.IsError)
            {
                return checkBlockStatus.Errors;
            }

            ErrorOr<bool> checkActiveStatus = await CheckActiveStatus(user, ct);
            if (checkActiveStatus.IsError)
            {
                return checkActiveStatus.Errors;
            }

            ErrorOr<bool> checkPassword = await CheckPassword(user, command.Password, ct);
            if (checkPassword.IsError)
            {
                return checkPassword.Errors;
            }

            await ResetFailedStatusEvent(user, ct);

            return user;
        }
    }
}