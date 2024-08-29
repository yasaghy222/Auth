using FluentValidation;

namespace Auth.Features.Users.EndPoints.ChangePassword
{
    public class UserChangePasswordValidation : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidation()
        {
            RuleFor(i => i.Id).NotEmpty().NotNull();

            RuleFor(i => i.OldPassword)
                           .NotNull()
                           .NotEmpty()
                           .MaximumLength(500);

            RuleFor(i => i.Password)
                .NotNull()
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
                .NotNull()
                .NotEmpty()
                .Equal(i => i.Password).WithMessage("Repeat Password' must be equal to Password");
        }
    }
}