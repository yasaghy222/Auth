using Auth.Contracts.Request;

namespace Auth.Features.Users.Contracts.Requests
{
    public record IUserCommand<TResponse> : ICommand<TResponse>;
}