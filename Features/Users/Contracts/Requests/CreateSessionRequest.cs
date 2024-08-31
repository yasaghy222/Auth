using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.Contracts.Requests
{
    public record CreateSessionRequest
    {
        public Ulid Id { get; set; }

        public Ulid UserId { get; set; }
        public SessionPlatform Platform { get; set; }

        public required string UniqueId { get; set; }
        public required string IP { get; set; }

        public required Ulid OrganizationId { get; set; }
        public required string OrganizationTitle { get; set; }

        public DateTime ExpireAt { get; set; }
    }
}