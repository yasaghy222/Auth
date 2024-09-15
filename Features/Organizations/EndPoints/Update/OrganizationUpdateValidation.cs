using FluentValidation;

namespace Auth.Features.Organizations.EndPoints.Update
{
    public class UserUpdateValidation
        : AbstractValidator<OrganizationUpdateDto>
    {
        public UserUpdateValidation()
        {
            RuleFor(i => i.Id)
              .NotNull();

            RuleFor(i => i.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(i => i.ParentId)
                .NotEmpty();
        }
    }
}