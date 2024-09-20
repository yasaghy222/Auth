using MediatR;
using Auth.Features.Users.Repositories;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Mappings;

namespace Auth.Features.Users.Events.LoginFailed
{
    public class LoginFailedEventHandler(
        IUserRepository userRepository,
        ILogger<LoginFailedEventHandler> logger)
        : INotificationHandler<LoginFailedEvent>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<LoginFailedEventHandler> _logger = logger;

        public async Task Handle(LoginFailedEvent @event, CancellationToken ct)
        {
            LoginFailedRequest loginFailedRequest = @event.MapToRequest();

            await _userRepository.UpdateFailedLoginInfoAsync(loginFailedRequest, ct);

            _logger.LogWarning("Login failed for user: {@event.Username}, Reason: {@event.Reason}",
                @event.Username, @event.Reason);
        }
    }
}
