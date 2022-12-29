using FluentValidation;

namespace Makku.Shelters.Application.Shelters.Profile.Commands.UpdateProfile
{
    internal class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(updateShelterCommand=>updateShelterCommand.Name).NotEmpty();
            RuleFor(updateShelterCommand => updateShelterCommand.UserId).NotEqual(Guid.Empty).NotNull();
            RuleFor(updateShelterCommand => updateShelterCommand.Id).NotEqual(Guid.Empty).NotNull();
        }
    }
}
