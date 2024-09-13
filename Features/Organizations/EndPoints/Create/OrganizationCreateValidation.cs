using FluentValidation;

namespace Auth.Features.Organizations.EndPoints.Create
{
    public class OrganizationCreateValidation : AbstractValidator<OrganizationCreateDto>
    {
        public OrganizationCreateValidation()
        {
            RuleFor(i => i.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(i => i.ParentId)
                .NotEmpty();
        }
    }
}