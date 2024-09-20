namespace Auth.Features.Roles.Contracts.Requests
{
    public record UpdateRequest
    {
        public required Ulid Id { get; set; }
        public required string Title { get; set; }
    }
}