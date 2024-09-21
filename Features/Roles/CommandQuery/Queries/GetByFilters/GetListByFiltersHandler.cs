using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Roles.Repositories;
using Auth.Features.Roles.Contracts.Requests;
using Auth.Features.Roles.Contracts.Mappings;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Organizations.Repositories;
using Auth.Features.Roles.Contracts.Responses;
using Auth.Features.Organizations.Contracts.Mappings;

namespace Auth.Features.Roles.CommandQuery.Queries.GetByFilters
{
    public class GetListByFiltersHandler(
        IUserClaimsInfo userClaimsInfo,
        IRoleRepository roleRepository,
        IOrganizationRepository organizationRepository)
        : IRequestHandler<GetListByFiltersQuery, ErrorOr<RolesResponse>>
    {
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IOrganizationRepository _organizationRepository = organizationRepository;

        private async Task<ErrorOr<bool>> ValidateOrganization(Ulid organizationId, CancellationToken ct)
        {
            Option<Organization> organization = await _organizationRepository
                .FindAsync(i => i.Id == organizationId, ct);

            if (organization.IsNone)
            {
                return OrganizationErrors.NotFound();
            }

            Ulid[] parentIds = await _organizationRepository
                .GetParentIdsAsync(organization.ValueUnsafe());

            IEnumerable<Ulid> userOrganizations = _userClaimsInfo
                .UserOrganizations?.Select(i => i.OrganizationId) ?? [];

            if (userOrganizations.Any(parentIds.Contains))
            {
                return true;
            }

            return Error.Forbidden();
        }

        public async Task<ErrorOr<RolesResponse>> Handle(
            GetListByFiltersQuery query, CancellationToken ct)
        {
            ErrorOr<bool> validateOrganization =
                await ValidateOrganization(query.OrganizationId, ct);

            if (validateOrganization.IsError)
            {
                return validateOrganization.Errors;
            }

            RoleFilterRequest request = query.MapToRequest();
            return await _roleRepository.ToListByFilters(request, ct);
        }
    }
}