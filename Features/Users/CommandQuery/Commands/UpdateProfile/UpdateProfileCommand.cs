using Auth.Features.Users.Contracts.Requests;
using ErrorOr;

namespace Auth.Features.Users.CommandQuery.Commands.UpdateProfile
{
    public record UpdateProfileCommand
        : IUserCommand<ErrorOr<Updated>>
    {
        public required string Name { get; set; }
        public required string Family { get; set; }

        public required string Username { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
    }
}