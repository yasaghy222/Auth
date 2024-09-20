using FluentValidation;

namespace Auth.Features.Organizations.EndPoints.Update
{
    public class OrganizationUpdateValidation
        : AbstractValidator<OrganizationUpdateDto>
    {
        public OrganizationUpdateValidation()
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