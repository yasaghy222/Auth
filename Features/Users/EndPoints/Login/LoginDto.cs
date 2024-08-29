using Auth.Features.Sessions.Contracts.Enums;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.EndPoints.Login
{
    public record LoginDto
    {
        public required Ulid OrganizationId { get; set; }
        public required string Username { get; set; }

        public UserLoginType Type { get; set; } = UserLoginType.Password;

        public string UniqueId { get; set; } = Ulid.NewUlid(DateTime.UtcNow).ToString();
        public required SessionPlatform Platform { get; set; } = SessionPlatform.Web;
        public string? Password { get; set; }

    }
}