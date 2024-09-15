namespace Auth.Features.Organizations.Contracts.Requests
{
    public record UpdateRequest
    {
        public required Ulid Id { get; set; }
        public required string Title { get; set; }
        public required Ulid ParentId { get; set; }
    }
}