using Auth.Contracts.Response;

namespace Auth.Features.Users.Contracts.Responses
{
    public record UsersResponse : QueryResponse<UserResponse>;
}