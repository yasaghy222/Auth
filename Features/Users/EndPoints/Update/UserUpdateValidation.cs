using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Features.Users.EndPoints.Update
{
    public class UserUpdateValidation
        : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidation()
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
        }
    }
}