using ErrorOr;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.CommandQuery.Queries.GetById
{
    public record GetByIdQuery(Ulid Id, IEnumerable<Ulid> OrganizationIds)
    : IUserQuery<ErrorOr<OrganizationResponse>>;
}