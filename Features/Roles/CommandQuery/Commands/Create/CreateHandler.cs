using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Roles.Repositories;
using Auth.Features.Roles.Contracts.Mappings;
using Auth.Features.Organizations.Repositories;

namespace Auth.Features.Roles.CommandQuery.Commands.Create
{
    public class CreateHandler(
        IUserClaimsInfo userClaimsInfo,
        IRoleRepository RoleRepository,
        IOrganizationRepository OrganizationRepository) :
        IRequestHandler<CreateCommand, ErrorOr<Ulid>>
    {
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;
        private readonly IRoleRepository _roleRepository = RoleRepository;
        private readonly IOrganizationRepository _organizationRepository = OrganizationRepository;

        private async Task<ErrorOr<bool>> ValidateTitle(CreateCommand command, CancellationToken ct)
        {
            Option<string> existingRole = await _roleRepository.FindAsync(
               expression: i => i.Title == command.Title
                && i.OrganizationId == command.OrganizationId,
               selector: i => i.Title, ct);

            if (existingRole.IsSome)
            {
                string existRole = existingRole.ValueUnsafe();

                if (existRole == command.Title)
                {
                    return RoleErrors.DuplicateTitle(command.Title);
                }
            }

            return true;
        }

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

        public async Task<ErrorOr<Ulid>> Handle(CreateCommand command, CancellationToken ct)
        {
            ErrorOr<bool> validateOrganization = await ValidateOrganization(command.OrganizationId, ct);
            if (validateOrganization.IsError)
            {
                return validateOrganization.Errors;
            }

            ErrorOr<bool> validateTitle = await ValidateTitle(command, ct);
            if (validateTitle.IsError)
            {
                return validateTitle.Errors;
            }

            Role role = command.MapToEntity();
            await _roleRepository.AddAsync(role, ct);

            return role.Id;
        }
    }
}