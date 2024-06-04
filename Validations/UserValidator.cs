using AuthenticateDTOs;
using FluentValidation;

namespace Authenticate.Validations {
    public class UserValidator: AbstractValidator<UserDto>
    {

        public UserValidator()
        {
            RuleFor(i => i.OrganizationId)
                .NotEmpty()
                .NotNull();

            RuleFor(i => i.Username)
                .NotEmpty()
                .NotNull()
                .Length(3, 30);
            
            RuleFor(i => i.Password)
                .NotEmpty()
                .NotNull()
                .Length(3, 30);
        }
        
    }
}