using ErrorOr;
using Auth.Features.Users.Contracts.Requests;

namespace Auth.Features.Users.CommandQuery.Commands.Update
{
    public record UpdateCommand
        : IUserCommand<ErrorOr<Updated>>
    {
        public required Ulid Id { get; set; }
        public required string Name { get; set; }
        public required string Family { get; set; }

        public required string Username { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
    }
}