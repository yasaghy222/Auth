namespace Auth.Features.Users.EndPoints.UpdateProfile
{
    public record UserUpdateProfileDto
    {
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
    }
}