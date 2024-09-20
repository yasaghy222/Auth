using FluentValidation;

namespace Auth.Features.Roles.EndPoints.Update
{
    public class RoleUpdateValidation
        : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateValidation()
        {
            RuleFor(i => i.Id)
              .NotNull();

            RuleFor(i => i.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(i => i.OrganizationId)
                .NotEmpty();
        }
    }
}