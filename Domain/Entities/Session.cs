using Auth.Domain.Aggregates;
using Auth.Features.Sessions.Contracts.Enums;

namespace Auth.Domain.Entities
{
    public class Session : BaseAggregate<Ulid>
    {
        public required Ulid UserId { get; set; }
        public User? User { get; set; }

        public SessionPlatform Platform { get; set; }

        public required string UniqueId { get; set; }
        public required string IP { get; set; }

        public required Ulid OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public DateTime ExpireAt { get; set; } = DateTime.UtcNow.AddDays(7);
    }
}