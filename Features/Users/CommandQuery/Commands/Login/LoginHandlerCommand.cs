using Auth.Domain.Entities;
using Auth.Features.Users.Contracts.Requests;
using ErrorOr;

namespace Auth.Features.Users.CommandQuery.Commands.Login
{
    public record LoginHandlerCommand : IUserCommand<ErrorOr<User>>
    {
        public string Username { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}