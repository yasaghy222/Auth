namespace Auth.Features.Users.EndPoints.ChangePassword
{
    public record UserChangePasswordDto
    {
        public Ulid Id { get; set; }
        public required string OldPassword { get; set; }
        public required string Password { get; set; }
        public required string RepeatPassword { get; set; }
    }
}