namespace Auth.Features.Users.EndPoints.ChangePassword
{
    public record UserChangePasswordDto
    {
        public Ulid Id { get; set; }
        public string? OldPassword { get; set; }
        public string? Password { get; set; }
        public string? RepeatPassword { get; set; }
    }
}