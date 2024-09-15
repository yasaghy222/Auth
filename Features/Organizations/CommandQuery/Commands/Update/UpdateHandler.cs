using ErrorOr;
using MediatR;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Shared.CustomErrors;
using LanguageExt.UnsafeValueAccess;
using Auth.Features.Organizations.Repositories;
using Auth.Features.Organizations.Contracts.Mappings;
using Auth.Features.Organizations.Contracts.Requests;

namespace Auth.Features.Organizations.CommandQuery.Commands.Update
{
    public class UpdateHandler(
        IUserClaimsInfo userClaimsInfo,
        IOrganizationRepository organizationRepository)
        : IRequestHandler<UpdateCommand, ErrorOr<Updated>>
    {
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;
        private readonly IOrganizationRepository _organizationRepository = organizationRepository;

        private async Task<ErrorOr<bool>> ValidateTitle(string title, CancellationToken ct)
        {
            Option<string> existingOrganization = await _organizationRepository.FindAsync(
               expression: i => i.Title == title,
               selector: i => i.Title, ct);

            if (existingOrganization.IsSome)
            {
                string existOrganization = existingOrganization.ValueUnsafe();

                if (existOrganization == title)
                {
                    return OrganizationErrors.DuplicateTitle(title);
                }
            }

            return true;
        }

        private async Task<ErrorOr<bool>> ValidateParent(Ulid parentId, CancellationToken ct)
        {
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
        public async Task<ErrorOr<Updated>> Handle(
            UpdateCommand command, CancellationToken ct)
        {
            ErrorOr<bool> validateTitle = await ValidateTitle(command.Title, ct);
            if (validateTitle.IsError)
            {
                return validateTitle.Errors;
            }

            ErrorOr<bool> validateParent = await ValidateParent(command.ParentId, ct);
            if (validateParent.IsError)
            {
                return validateParent.Errors;
            }

            UpdateRequest updateRequest = command.MapToRequest();
            await _organizationRepository.UpdateAsync(updateRequest, ct);

            return Result.Updated;
        }
    }
}