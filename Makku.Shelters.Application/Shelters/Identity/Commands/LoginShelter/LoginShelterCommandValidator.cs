using FluentValidation;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.LoginShelter
{
    public class LoginShelterCommandValidator : AbstractValidator<LoginShelterCommand>
    {
        public LoginShelterCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotNull()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("Provided string is not a correct email address format");
        }
    }
}
