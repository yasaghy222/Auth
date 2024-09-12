using Auth.Features.Organizations.Contracts.Responses;
using Auth.Features.Organizations.Contracts.Mappings;
using Auth.Features.Organizations.Contracts.Requests;
using Auth.Features.Organizations.Repositories;
using Auth.Features.Users.Contracts.Mappings;
using MediatR;

namespace Auth.Features.Organizations.CommandQuery.Queries.GetByFilters
{
    public class GetListByFiltersHandler(
        IOrganizationRepository organizationRepository)
        : IRequestHandler<GetListByFiltersQuery, OrganizationsResponse>
    {
        private readonly IOrganizationRepository _organizationRepository
             = organizationRepository;

        public async Task<OrganizationsResponse> Handle(
            GetListByFiltersQuery query, CancellationToken ct)
        {
            OrganizationFilterRequest request = query.MapToRequest();
            return await _organizationRepository.ToListByFilters(request, ct);
        }
    }
}