using AuthenticateDTOs;
using FluentValidation;

namespace Authenticate.Validations {
    public class UserValidator: AbstractValidator<UserDto>
    {

        public UserValidator()
        {
            RuleFor(i => i.Username).NotEmpty();
            RuleFor(i => i.Username).NotNull();
            RuleFor(i => i.Username).Length(3, 30);
            
            RuleFor(i => i.Password).NotEmpty();
            RuleFor(i => i.Password).NotNull();
            RuleFor(i => i.Password).Length(3, 30);
        }
        
    }
}