namespace Auth.Features.Roles.EndPoints.Update
{
    public record RoleUpdateDto
    {
        public required Ulid Id { get; set; }
        public required string Title { get; set; }
        public required Ulid OrganizationId { get; set; }
    }
}