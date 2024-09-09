namespace Auth.Features.Users.EndPoints.Create
{
    public record UserCreateDto
    {
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string Username { get; set; }

        public required string Password { get; set; }
        public required string RepeatPassword { get; set; }

        public required string Phone { get; set; }
        public string? Email { get; set; }
    }
}