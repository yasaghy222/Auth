namespace Auth.Features.Organizations.EndPoints.Create
{
    public record OrganizationCreateDto
    {
        public required string Title { get; set; }
        public required Ulid ParentId { get; set; }
    }
}