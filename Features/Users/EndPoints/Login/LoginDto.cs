using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.EndPoints.Login
{
    public record LoginDto
    {
        public Ulid OrganizationId { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }

        public UserLoginType Type { get; set; } = UserLoginType.Password;

        public string? UniqueId { get; set; }
        public required SessionPlatform Platform { get; set; } = SessionPlatform.Web;
    }
}