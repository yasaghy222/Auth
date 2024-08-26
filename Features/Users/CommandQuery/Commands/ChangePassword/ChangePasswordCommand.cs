using Auth.Features.Users.Contracts.Requests;
using ErrorOr;

namespace Auth.Features.Users.CommandQuery.Commands.ChangePassword
{
    public record ChangePasswordCommand : IUserCommand<ErrorOr<Updated>>
    {
        public Ulid Id { get; set; }
        public required string OldPassword { get; set; }
        public required string Password { get; set; }
    }
}