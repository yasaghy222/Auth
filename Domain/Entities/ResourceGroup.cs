using Auth.Domain.Aggregates;

namespace Auth.Domain.Entities
{
    public class ResourceGroup : BaseAggregate<Ulid>
    {
        public required string Title { get; set; }
        public int? Order { get; set; }

        public Ulid? ParentId { get; set; }
        public ResourceGroup? Parent { get; set; }

        public ICollection<ResourceGroup>? Chields { get; set; }
        public ICollection<Resource>? Resources { get; set; }
    }
}