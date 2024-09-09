using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Shared.CustomErrors;
using Auth.Features.Organizations.Repositories;
using Auth.Features.Organizations.Contracts.Responses;
using Auth.Features.Organizations.Contracts.Mappings;

namespace Auth.Features.Organizations.CommandQuery.Queries.GetById
{
    public class GetByIdHandler(IOrganizationRepository organizationRepository)
        : IRequestHandler<GetByIdQuery, ErrorOr<OrganizationResponse>>
    {
        private readonly IOrganizationRepository _organizationRepository
             = organizationRepository;

        public async Task<ErrorOr<OrganizationResponse>> Handle(
            GetByIdQuery query, CancellationToken ct)
        {
            Option<Organization> organization = await _organizationRepository
                .FindAsync(i => i.Id == query.Id, query.OrganizationIds, ct);

            return organization.Match<ErrorOr<OrganizationResponse>>(
                value => value.MapToResponse(),
                () => OrganizationErrors.NotFound());
        }
    }
}