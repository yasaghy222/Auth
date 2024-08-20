using ErrorOr;
using Auth.Features.Users.Contracts.Requests;

namespace Auth.Features.Users.CommandQuery.Commands.Create
{
    public record CreateCommand : IUserCommand<ErrorOr<Created>>
    {
        public required string Name { get; set; }
        public required string Family { get; set; }

        public required string Username { get; set; }
        public required string Password { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}