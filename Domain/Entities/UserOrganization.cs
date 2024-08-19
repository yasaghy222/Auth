using Auth.Domain.Aggregates;

namespace Auth.Domain.Entities
{
    public class UserOrganization : BaseAggregate<Ulid>
    {
        public required Ulid UserId { get; set; }
        public User? User { get; set; }

        public required Ulid OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public required Ulid RoleId { get; set; }
        public Role? Role { get; set; }
    }
}