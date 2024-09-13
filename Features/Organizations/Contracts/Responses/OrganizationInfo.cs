namespace Auth.Features.Organizations.Contracts.Responses
{
    public record OrganizationInfo
    {
        public Ulid Id { get; set; }
        public required string Title { get; set; }


        public IEnumerable<OrganizationInfo> Chides { get; set; } = [];
        public Ulid[] ChidesIds { get; set; } = [];
        public Ulid[] ParentIds { get; set; } = [];
    }
}