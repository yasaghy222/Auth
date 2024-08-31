using Auth.Domain.Entities;
using Auth.Features.Users.EndPoints.Login;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Events.LoginFailed;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.CommandQuery.Commands.Login;
using Auth.Features.Users.Events.ResetFailedStatus;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class UserLoginMappings
    {
        public static LoginCommand MapToCommand(
            this LoginDto dto, string ip)
        {
            return new()
            {
                OrganizationId = dto.OrganizationId,
                UniqueId = dto.UniqueId,
                IP = ip,
                Username = dto.Username,
                Platform = dto.Platform,
                Type = dto.Type,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
            };
        }

        public static LoginHandlerCommand MapToHandlerCommand(
            this LoginCommand command)
        {
            return new()
            {
                Username = command.Username,
                Phone = command.Phone,
                Email = command.Email,
                Password = command.Password,
            };
        }

        public static LoginFailedEvent MapToEvent(this User user, string reason)
        {
            return new()
            {
                Id = user.Id,
                Username = user.Username,
                Reason = reason,
                FailedLoginAttempts = user.FailedLoginAttempts,
            };
        }

        public static ResetFailedStatusEvent MapToEvent(this User user)
        {
            return new()
            {
                Id = user.Id,
                Username = user.Username,
            };
        }

        public static LoginFailedRequest MapToRequest(
            this LoginFailedEvent @event)
        {
            return new()
            {
                Id = @event.Id,
                FailedLoginAttempts = @event.FailedLoginAttempts + 1,
                AccountLockedUntil = @event.FailedLoginAttempts >= 4
                    ? DateTime.UtcNow.AddMinutes(5) : null,
                Status = @event.FailedLoginAttempts >= 4
                    ? UserStatus.Blocked : UserStatus.Active
            };
        }

        public static CreateSessionRequest MapToRequest(
            this LoginCommand loginCommand,
            Ulid sessionId,
            Ulid userId,
            string ip,
            string organizationTitle,
            DateTime expireAt)
        {
            return new()
            {
                Id = sessionId,
                IP = ip,
                OrganizationId = loginCommand.OrganizationId,
                OrganizationTitle = organizationTitle,
                UniqueId = loginCommand.UniqueId,
                Platform = loginCommand.Platform,
                ExpireAt = expireAt,
                UserId = userId,
            };
        }

    }
}