using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Features.Users.EndPoints.Create
{
    public class UserCreateValidation : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidation()
        {
            RuleFor(i => i.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(i => i.Family)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(i => i.Username)
                .NotNull()
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(i => i.Phone)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(\+98|0)?9\d{9}$");

            RuleFor(i => i.Email)
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
                .When(i => !i.Email.IsNullOrEmpty());

            RuleFor(i => i.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage(@"
                    Password must be at least 8 characters long
                    and contain at least one uppercase letter,
                    one lowercase letter, one digit,
                    and one special character.");

            RuleFor(i => i.RepeatPassword)
                .NotNull()
                .NotEmpty()
                .Equal(i => i.Password).WithMessage("Repeat Password' must be equal to Password");
        }
    }
}