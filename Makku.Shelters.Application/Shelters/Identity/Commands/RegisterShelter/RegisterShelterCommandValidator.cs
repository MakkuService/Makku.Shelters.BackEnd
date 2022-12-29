using FluentValidation;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.RegisterShelter
{
    internal class RegisterShelterCommandValidator : AbstractValidator<RegisterShelterCommand>
    {
        public RegisterShelterCommandValidator()
        {
            RuleFor(c => c.Username)
                .NotNull()
                .WithMessage("Username name is required. It is currently null")
                .MinimumLength(4)
                .WithMessage("Username name must be at least 4 characters long")
                .MaximumLength(10)
                .WithMessage("Username name can contain at most 10 characters long");
            RuleFor(c => c.ShelterName)
                .NotNull()
                .WithMessage("ShelterName name is required. It is currently null")
                .MinimumLength(3)
                .WithMessage("ShelterName name must be at least 3 characters long")
                .MaximumLength(100)
                .WithMessage("ShelterName name can contain at most 100 characters long");
            RuleFor(c => c.Email)
                .NotNull()
                .WithMessage("Email address is required")
                .EmailAddress()
                .WithMessage("Provided string is not a correct email address format");
        }
    }
}
