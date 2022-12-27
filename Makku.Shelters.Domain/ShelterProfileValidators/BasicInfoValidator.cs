using FluentValidation;
using Makku.Shelters.Domain.ShelterProfileAggregate;

namespace Makku.Shelters.Domain.ShelterProfileValidators
{
    internal class BasicInfoValidator : AbstractValidator<BasicInfo>
    {
        public BasicInfoValidator()
        {
            RuleFor(info => info.ShelterName)
                .NotNull().WithMessage("ShelterName name is required. It is currently null")
                .MinimumLength(3).WithMessage("ShelterName name must be at least 3 characters long")
                .MaximumLength(50).WithMessage("ShelterName name can contain at most 50 characters long");
            RuleFor(info => info.Email)
                .NotNull().WithMessage("Email address is required")
                .EmailAddress().WithMessage("Provided string is not a correct email address format");
        }
    }
}
