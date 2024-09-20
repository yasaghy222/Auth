using Auth.Contracts.Request;

namespace Auth.Features.Roles.Contracts.Requests
{
    public record IRoleQuery<TResponse> : IQuery<TResponse>;
}