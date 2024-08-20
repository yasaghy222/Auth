namespace Auth.Features.Users.EndPoints.Update
{
    public record UpdateDto
    {
        public required Ulid Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}