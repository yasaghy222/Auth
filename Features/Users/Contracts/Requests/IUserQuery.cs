using Auth.Contracts.Request;

namespace Auth.Features.Users.Contracts.Requests
{
    public record IUserQuery<TResponse> : IQuery<TResponse>;
}