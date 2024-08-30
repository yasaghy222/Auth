using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.Contracts.Responses
{
    public record SessionResponse
    {
        public SessionPlatform Platform { get; set; }

        public required string UniqueId { get; set; }
        public required string IP { get; set; }

        public required Ulid OrganizationId { get; set; }
        public required string OrganizationTitle { get; set; }

        public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddDays(7);
    }

    public record SessionsResponse
    {
        public IEnumerable<SessionResponse> Items { get; set; } = [];
    }
}