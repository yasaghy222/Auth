namespace Auth.Features.Organizations.EndPoints.Update
{
    public record OrganizationUpdateDto
    {
        public required Ulid Id { get; set; }
        public required string Title { get; set; }
        public required Ulid ParentId { get; set; }
    }
}