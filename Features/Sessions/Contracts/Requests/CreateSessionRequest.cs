using Auth.Features.Sessions.Contracts.Enums;

namespace Auth.Features.Sessions.Contracts.Requests
{
    public record CreateSessionRequest
    {
        public Ulid UserId { get; set; }
        public SessionPlatform Platform { get; set; }

        public required string UniqueId { get; set; }
        public required string IP { get; set; }

        public required Ulid OrganizationId { get; set; }
        public required string OrganizationTitle { get; set; }

        public DateTime ExpireAt { get; set; }
    }
}