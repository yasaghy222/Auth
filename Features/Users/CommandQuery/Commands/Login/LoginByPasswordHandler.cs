using Auth.Domain.Entities;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.Events.LoginFailed;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Services;
using Auth.Shared.CustomErrors;
using Auth.Shared.Extensions;
using ErrorOr;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MediatR;

namespace Auth.Features.Users.CommandQuery.Commands.Login
{
    public class LoginByPasswordHandler(
        IUserRepository userRepository,
        IHashService hashService,
        IMediator mediator) :
        ILoginHandler
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHashService _hashService = hashService;
        private readonly IMediator _mediator = mediator;


        public async Task<ErrorOr<TokenResponse>> HandleLogin(LoginCommand command, CancellationToken ct)
        {
            Option<User> getUser = await _userRepository
                .FindAsync(i => i.Username == command.Username, ct);

            if (getUser.IsNone)
            {
                return UserErrors.InvalidUserPass();
            }

            User user = getUser.ValueUnsafe();
            LoginFailedEvent loginFailedEvent = user.MapToEvent();


            if (user.Status != UserStatus.Active)
            {
                loginFailedEvent.Reason = "User account is suspended";
                await _mediator.Publish(loginFailedEvent, ct);

                return UserErrors.SuspendStatus();
            }

            if (_hashService.VerifyHashString(user.Password, command.Password))
            {
                loginFailedEvent.Reason = "Invalid password";
                await _mediator.Publish(loginFailedEvent, ct);

                return UserErrors.InvalidUserPass();
            }


            //submit session event


        }
    }
}