using ErrorOr;
using Auth.Features.Organizations.Contracts.Requests;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.CommandQuery.Queries.GetById
{
    public record GetByIdQuery(Ulid Id)
    : IOrganizationQuery<ErrorOr<OrganizationResponse>>;
}