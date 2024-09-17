
using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Organizations.Services;
using Auth.Features.Organizations.Repositories;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.CommandQuery.Commands.Delete
{
    public class DeleteHandler(
        IOrganizationRepository OrganizationRepository,
        IUserClaimsInfo userClaimsInfo) :
        IRequestHandler<DeleteCommand, ErrorOr<Deleted>>
    {
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;
        private readonly IOrganizationRepository _organizationRepository
             = OrganizationRepository;

        private async Task<ErrorOr<bool>> ValidateParent(Ulid? parentId, CancellationToken ct)
        {
            if (parentId == null)
                return true;

            Option<Organization> parent = await _organizationRepository
                .FindAsync(i => i.Id == parentId, [], ct);

            if (parent.IsNone)
            {
                return OrganizationErrors.ParentNotFound();
            }

            Ulid[] parentIds = await _organizationRepository
                .GetParentIdsAsync(parent.ValueUnsafe());

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
            IEnumerable<Ulid> initialOrganizationIds =
                 OrganizationDataSeeding.InitialItems.Select(i => i.Id);

            if (initialOrganizationIds.Contains(command.Id))
            {
                return OrganizationErrors.CanNotDelInitialItem();
            }

            Option<OrganizationInfo> getOrganization = await _organizationRepository
                .GetInfoAsync(command.Id, ct);

            if (getOrganization.IsNone)
            {
                return OrganizationErrors.NotFound();
            }

            OrganizationInfo organizationInfo = getOrganization.ValueUnsafe();

            ErrorOr<bool> validateParent = await ValidateParent(organizationInfo.ParentId, ct);
            if (validateParent.IsError)
            {
                return validateParent.Errors;
            }

            if (organizationInfo.ChildrenIds.Length > 0)
            {
                return OrganizationErrors.CanNotDelParent();
            }

            bool isDeleted = await _organizationRepository.DeleteAsync(command.Id, ct);
            return isDeleted ? Result.Deleted : OrganizationErrors.NotFound();
        }
    }
}