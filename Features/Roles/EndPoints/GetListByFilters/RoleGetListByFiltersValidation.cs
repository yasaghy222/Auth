using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Features.Roles.EndPoints.GetListByFilters
{
    public class RoleGetListByFiltersValidation : AbstractValidator<RoleGetListByFiltersDto>
    {
        public RoleGetListByFiltersValidation()
        {
            RuleFor(i => i.OrganizationId)
                .NotEmpty();

            RuleFor(i => i.Title).MaximumLength(100)
                .When(i => !i.Title.IsNullOrEmpty());

            RuleFor(x => (int)x.TittleComparisonType)
                .InclusiveBetween(1, 4)
                .WithMessage("The TittleComparisonType must be between 1 and 4.");

            RuleFor(x => x.Status).IsInEnum();
        }
    }
}