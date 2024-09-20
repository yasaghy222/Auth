namespace Auth.Features.Roles.EndPoints.Create
{
    public record RoleCreateDto
    {
        public required string Title { get; set; }
        public required Ulid OrganizationId { get; set; }
    }
}