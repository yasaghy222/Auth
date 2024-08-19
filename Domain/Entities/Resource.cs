using Auth.Domain.Aggregates;

namespace Auth.Domain.Entities
{
    public class Resource : BaseAggregate<Ulid>
    {
        public required string Title { get; set; }

        public required Ulid OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public required string Url { get; set; }

        public Ulid? GroupId { get; set; }
        public ResourceGroup? Group { get; set; }

        public ICollection<Permission>? Permissions { get; set; }
    }
}