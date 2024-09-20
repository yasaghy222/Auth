using FluentValidation;

namespace Auth.Features.Roles.EndPoints.Create
{
    public class RoleCreateValidation : AbstractValidator<RoleCreateDto>
    {
        public RoleCreateValidation()
        {
            RuleFor(i => i.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(i => i.OrganizationId)
                .NotEmpty();
        }
    }
}