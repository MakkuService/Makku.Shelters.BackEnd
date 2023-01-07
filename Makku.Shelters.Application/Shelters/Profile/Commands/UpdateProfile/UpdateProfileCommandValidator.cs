using FluentValidation;

namespace Makku.Shelters.Application.Shelters.Profile.Commands.UpdateProfile
{
    internal class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(updateShelterCommand=>updateShelterCommand.ShelterName).NotEmpty();
            RuleFor(updateShelterCommand => updateShelterCommand.IdentityShelterId).NotEqual(Guid.Empty).NotNull();
            RuleFor(updateShelterCommand => updateShelterCommand.RequestorGuid).NotEqual(Guid.Empty).NotNull();
        }
    }
}
