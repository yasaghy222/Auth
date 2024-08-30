using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.EndPoints.Login
{
    public record LoginDto
    {
        public required Ulid OrganizationId { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserLoginType Type { get; set; } = UserLoginType.Password;

        public string UniqueId { get; set; } = Ulid.NewUlid(DateTime.UtcNow).ToString();
        public required SessionPlatform Platform { get; set; } = SessionPlatform.Web;
    }
}