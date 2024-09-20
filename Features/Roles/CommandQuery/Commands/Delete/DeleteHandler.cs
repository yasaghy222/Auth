
using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using Auth.Features.Roles.Services;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Roles.Repositories;
using Auth.Features.Organizations.Repositories;

namespace Auth.Features.Roles.CommandQuery.Commands.Delete
{
    public class DeleteHandler(
        IUserClaimsInfo userClaimsInfo,
        IRoleRepository roleRepository,
        IOrganizationRepository organizationRepository) :
        IRequestHandler<DeleteCommand, ErrorOr<Deleted>>
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
        public async Task<ErrorOr<Deleted>> Handle(DeleteCommand command, CancellationToken ct)
        {
            IEnumerable<Ulid> initialRoleIds =
                 RolesDataSeeding.InitialItems.Select(i => i.Id);

            if (initialRoleIds.Contains(command.Id))
            {
                return RoleErrors.CanNotDelInitialItem();
            }

            Option<Ulid> getOrganizationId = await _roleRepository
                .FindAsync(i => i.Id == command.Id, i => i.OrganizationId, ct);

            if (getOrganizationId.IsNone)
            {
                return RoleErrors.NotFound();
            }

            ErrorOr<bool> validateOrganization = await ValidateOrganization(getOrganizationId.ValueUnsafe(), ct);
            if (validateOrganization.IsError)
            {
                return validateOrganization.Errors;
            }

            bool isDeleted = await _roleRepository.DeleteAsync(command.Id, ct);
            return isDeleted ? Result.Deleted : OrganizationErrors.NotFound();

            //TODO : if success delete all userOrganization with this role and logout them
        }
    }
}