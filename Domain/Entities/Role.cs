using Auth.Domain.Aggregates;
using Auth.Features.Roles.Contracts.Enums;

namespace Auth.Domain.Entities
{
    public class Role : StatusAggregate<Ulid, RoleStatus>
    {
        public required string Title { get; set; }

        public required Ulid OrganizationId { get; set; }
        public Organization? Organizations { get; set; }

        public ICollection<UserOrganization>? UserOrganizations { get; set; }

    }
}