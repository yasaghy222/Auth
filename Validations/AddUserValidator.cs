using Authenticate.DTOs;
using FluentValidation;

namespace Authenticate.Validations
{
    public class AddUserValidator : AbstractValidator<AddUserDto>
    {

        public AddUserValidator()
        {
            RuleFor(i => i.Username)
                .NotEmpty()
                .NotNull()
                .Length(3, 30);

            RuleFor(i => i.Phone)
                        .Matches(@"^(\+98|0)?9\d{9}$")
                        .WithMessage("Phone number is Not Valid!");

            RuleFor(d => d.Email).EmailAddress().When(i => !string.IsNullOrEmpty(i.Email));

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