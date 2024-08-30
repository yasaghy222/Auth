using ErrorOr;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.CommandQuery.Commands.Login
{
    public record LoginCommand : IUserCommand<ErrorOr<TokenResponse>>
    {
        public required Ulid OrganizationId { get; set; }

        public required string IP { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserLoginType Type { get; set; } = UserLoginType.Password;

        public string UniqueId { get; set; } = Ulid.NewUlid(DateTime.UtcNow).ToString();
        public required SessionPlatform Platform { get; set; } = SessionPlatform.Web;

        public string Password { get; set; } = string.Empty;
    }
}