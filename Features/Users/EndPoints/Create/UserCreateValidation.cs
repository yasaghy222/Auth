using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Features.Users.EndPoints.Create
{
    public class UserCreateValidation : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidation()
        {
            RuleFor(i => i.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(i => i.Family).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(i => i.Username).NotNull().NotEmpty().MaximumLength(300);

            RuleFor(i => i.Phone).Matches(@"^(\+98|0)?9\d{9}$")
                .When(i => !i.Phone.IsNullOrEmpty());

            RuleFor(i => i.Email).EmailAddress()
                .When(i => !i.Email.IsNullOrEmpty());


            RuleFor(i => i.Password).NotNull().NotEmpty().MaximumLength(500);
            RuleFor(i => i.RepeatPassword).NotNull().NotEmpty().Equal(i => i.Password);
        }
    }
}