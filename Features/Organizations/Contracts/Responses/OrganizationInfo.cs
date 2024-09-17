namespace Auth.Features.Organizations.Contracts.Responses
{
    public record OrganizationInfo
    {
        public Ulid Id { get; set; }
        public required string Title { get; set; }
        public required Ulid? ParentId { get; set; }
        public Ulid[] ChildrenIds { get; set; } = [];
        public Ulid[] ParentIds { get; set; } = [];
    }
}