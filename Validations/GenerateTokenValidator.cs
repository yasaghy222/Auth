using Authenticate.DTOs;
using FluentValidation;

namespace Authenticate.Validations
{
    public class GenerateTokenValidator : AbstractValidator<GenerateTokenDto>
    {

        public GenerateTokenValidator()
        {
            RuleFor(i => i.Platform).IsInEnum();
            RuleFor(i => i.UniqueId).NotEmpty().NotNull();

            RuleFor(i => i.OrganizationId).NotEmpty().NotNull();
            RuleFor(i => i.Username).NotEmpty().NotNull().Length(3, 30);
            RuleFor(i => i.Password).NotEmpty().NotNull().Length(3, 30);
        }

    }
}