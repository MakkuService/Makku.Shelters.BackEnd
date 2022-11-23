using FluentValidation;

namespace Makku.Shelters.Application.Commands.UpdateShelter
{
    internal class UpdateShelterCommandValidator : AbstractValidator<UpdateShelterCommand>
    {
        public UpdateShelterCommandValidator()
        {
            RuleFor(updateShelterCommand=>updateShelterCommand.Name).NotEmpty();
            RuleFor(updateShelterCommand => updateShelterCommand.UserId).NotEqual(Guid.Empty).NotNull();
            RuleFor(updateShelterCommand => updateShelterCommand.Id).NotEqual(Guid.Empty).NotNull();
        }
    }
}
