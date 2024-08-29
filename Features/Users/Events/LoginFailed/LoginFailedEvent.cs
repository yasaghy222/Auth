using MediatR;

namespace Auth.Features.Users.Events.LoginFailed
{
    public record LoginFailedEvent : INotification
    {
        public required Ulid Id { get; set; }
        public required string Username { get; set; }
        public int FailedLoginAttempts { get; set; }
        public required string Reason { get; set; }
    }
}