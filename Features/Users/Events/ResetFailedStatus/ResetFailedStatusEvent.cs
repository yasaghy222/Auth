using MediatR;

namespace Auth.Features.Users.Events.ResetFailedStatus
{
    public record ResetFailedStatusEvent : INotification
    {
        public required Ulid Id { get; set; }
        public required string Username { get; set; }
    }
}