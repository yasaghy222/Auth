using FluentValidation;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.EndPoints.Login
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(i => i.OrganizationId)
                .NotEmpty()
                .NotNull();

            RuleFor(i => i.Type)
                .NotEmpty()
                .NotNull()
                .IsInEnum();

            RuleFor(i => i.Platform)
                .NotEmpty()
                .NotNull()
                .IsInEnum();

            RuleFor(i => i.UniqueId)
                .NotEmpty()
                .NotNull()
                .MaximumLength(500);

            RuleFor(i => i.Phone)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(\+98|0)?9\d{9}$")
                .When(i => i.Type == UserLoginType.Phone);

            RuleFor(i => i.Email)
                .NotEmpty()
                .NotNull()
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
                .When(i => i.Type == UserLoginType.Email);

            RuleFor(i => i.Username)
                .NotEmpty()
                .NotNull()
                .MaximumLength(400)
                .When(i => i.Type == UserLoginType.Password);

            RuleFor(i => i.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500)
                .When(i => i.Type == UserLoginType.Password);
        }
    }
}