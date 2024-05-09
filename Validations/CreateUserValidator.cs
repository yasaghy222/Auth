using AuthenticateDTOs;
using FluentValidation;

namespace Authenticate.Validations {
    public class CreateUserValidator: AbstractValidator<CreateUserDto>
    {

        public CreateUserValidator()
        {
            RuleFor(i => i.Username)
                .NotEmpty()
                .NotNull()
                .Length(3, 30);
            
            RuleFor(i => i.Password)
                .NotEmpty()
                .NotNull()
                .Length(3, 30);

            RuleFor(i => i.OrganizationId)
                .NotEmpty()
                .NotNull();
        }
        
    }
}