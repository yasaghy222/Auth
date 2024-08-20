using Auth.Domain.Aggregates;
using Auth.Features.Organizations.Contracts.Enums;

namespace Auth.Domain.Entities
{
    public class Organization : StatusAggregate<Ulid, OrganizationStatus>
    {
        public required string Title { get; set; }

        public Ulid? ParentId { get; set; }
        public Organization? Parent { get; set; }

        public ICollection<Role>? Roles { get; set; }
        public ICollection<Resource>? Resources { get; set; }
        public ICollection<UserOrganization>? UserOrganizations { get; set; }
    }
}