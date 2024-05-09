using Authenticate.DTOs;
using FluentValidation;

namespace Authenticate.Validations {
    public class OrganizationValidator: AbstractValidator<OrganizationDto>
    {

        public OrganizationValidator()
        {
            RuleFor(i => i.Id)
                .NotEmpty()
                .NotNull();
            
            RuleFor(i => i.Name)
                .NotEmpty()
                .NotNull()
                .Length(3, 30);
        }
        
    }
}