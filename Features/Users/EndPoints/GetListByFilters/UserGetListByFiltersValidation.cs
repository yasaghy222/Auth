using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Features.Users.EndPoints.GetListByFilters
{
    public class UserGetListByFiltersValidation : AbstractValidator<UserGetListByFiltersDto>
    {
        public UserGetListByFiltersValidation()
        {
            RuleFor(i => i.FullName).MaximumLength(100).
                When(i => !i.FullName.IsNullOrEmpty());

            RuleFor(x => (int)x.FullNameComparisonType)
                .InclusiveBetween(1, 4)
                .WithMessage("The NameComparisonType must be between 1 and 4.");

            RuleFor(x => (int)x.UsernamesComparisonType)
                       .InclusiveBetween(1, 4)
                       .WithMessage("The UsernamesComparisonType must be between 1 and 4.");

            RuleFor(x => (int)x.PhonesComparisonType)
                                 .InclusiveBetween(1, 4)
                                 .WithMessage("The PhonesComparisonType must be between 1 and 4.");

            RuleFor(x => (int)x.EmailsComparisonType)
                .InclusiveBetween(1, 4)
                .WithMessage("The EmailsComparisonType must be between 1 and 4.");

            RuleFor(x => x.Status).IsInEnum();
        }
    }
}