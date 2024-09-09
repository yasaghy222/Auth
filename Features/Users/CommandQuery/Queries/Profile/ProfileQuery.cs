using ErrorOr;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.CommandQuery.Queries.Profile
{
    public record ProfileQuery(Ulid Id, IEnumerable<Ulid> OrganizationIds) : IUserQuery<ErrorOr<UserResponse>>;
}