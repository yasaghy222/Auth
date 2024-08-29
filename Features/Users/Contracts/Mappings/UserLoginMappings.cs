using Auth.Domain.Entities;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Events.LoginFailed;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class UserLoginMappings
    {
        public static LoginFailedEvent MapToEvent(
                    this User user)
        {
            return new()
            {
                Id = user.Id,
                Username = user.Username,
                Reason = "",
                FailedLoginAttempts = user.FailedLoginAttempts,
            };
        }

        public static LoginFailedRequest MapToRequest(
               this LoginFailedEvent @event)
        {
            return new()
            {
                Id = @event.Id,
                FailedLoginAttempts = @event.FailedLoginAttempts++,
                AccountLockedUntil = @event.FailedLoginAttempts >= 5
                    ? DateTime.UtcNow.AddMinutes(5) : default,
                Status = UserStatus.Active
            };
        }

    }
}