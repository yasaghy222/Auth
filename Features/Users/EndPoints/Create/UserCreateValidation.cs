using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Features.Users.EndPoints.Create
{
    public class UserCreateValidation : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidation()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(i => i.Family)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(i => i.Username)
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(i => i.Phone)
                .NotEmpty()
                .Matches(@"^(\+98|0)?9\d{9}$");

            RuleFor(i => i.Email)
                .EmailAddress()
                .When(i => !i.Email.IsNullOrEmpty());

            RuleFor(i => i.Password)
                .NotEmpty()
                .MaximumLength(500)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("""
                    Password must be at least 8 characters long
                    and contain at least one uppercase letter,
                    one lowercase letter, one digit,
                    and one special character.
                    """);

            RuleFor(i => i.RepeatPassword)
                .NotEmpty()
                .Equal(i => i.Password).WithMessage("Repeat Password' must be equal to Password");
        }
    }
}