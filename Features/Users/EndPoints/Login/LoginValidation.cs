using FluentValidation;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.EndPoints.Login
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(i => i.OrganizationId)
                .NotEmpty();

            RuleFor(i => i.Type)
                .NotEmpty()
                .IsInEnum();

            RuleFor(i => i.Platform)
                .NotEmpty()
                .IsInEnum();

            RuleFor(i => i.UniqueId)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(i => i.Phone)
                .NotEmpty()
                .Matches(@"^(\+98|0)?9\d{9}$")
                .When(i => i.Type == UserLoginType.Phone);

            RuleFor(i => i.Email)
                .NotEmpty()
                .EmailAddress()
                .When(i => i.Type == UserLoginType.Email);

            RuleFor(i => i.Username)
                .NotEmpty()
                .MaximumLength(400)
                .When(i => i.Type == UserLoginType.Password);

            RuleFor(i => i.Password)
                .NotEmpty()
                .MaximumLength(500)
                .When(i => i.Type == UserLoginType.Password);
        }
    }
}