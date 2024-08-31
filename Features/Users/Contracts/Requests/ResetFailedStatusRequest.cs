using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.Contracts.Requests
{
    public record ResetFailedStatusRequest
    {
        public required Ulid Id { get; set; }
    }
}