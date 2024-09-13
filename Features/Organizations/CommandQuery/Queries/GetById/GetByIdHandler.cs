using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Organizations.Repositories;
using Auth.Features.Organizations.Contracts.Mappings;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.CommandQuery.Queries.GetById
{
    public class GetByIdHandler(
        IOrganizationRepository organizationRepository,
        IUserClaimsInfo userClaimsInfo)
        : IRequestHandler<GetByIdQuery, ErrorOr<OrganizationResponse>>
    {
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;
        private readonly IOrganizationRepository _organizationRepository
             = organizationRepository;

        private async Task<ErrorOr<bool>> ValidateParentIds(Organization organization)
        {
            IEnumerable<Ulid> userOrganizationsIds =
                      _userClaimsInfo.UserOrganizations?.Select(i => i.OrganizationId) ?? [];

            Ulid[] parentIds = await _organizationRepository.GetParentIdsAsync(organization);

            if (userOrganizationsIds.Any(parentIds.Contains))
            {
                return Error.Forbidden();
            }

            return true;
        }

        public async Task<ErrorOr<OrganizationResponse>> Handle(
            GetByIdQuery query, CancellationToken ct)
        {
            Option<Organization> getOrganization = await _organizationRepository
                .FindAsync(i => i.Id == query.Id, null, ct);

            if (getOrganization.IsNone)
            {
                return OrganizationErrors.NotFound();
            }

            Organization organization = getOrganization.ValueUnsafe();

            ErrorOr<bool> validateParentIds = await ValidateParentIds(organization);
            if (validateParentIds.IsError)
            {
                return Error.Forbidden();
            }

            return organization.MapToResponse();
        }
    }
}