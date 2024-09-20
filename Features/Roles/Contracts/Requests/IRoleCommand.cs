using Auth.Contracts.Request;

namespace Auth.Features.Roles.Contracts.Requests
{
    public record IRoleCommand<TResponse> : ICommand<TResponse>;
}