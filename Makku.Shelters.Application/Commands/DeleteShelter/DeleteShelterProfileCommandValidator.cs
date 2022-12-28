using FluentValidation;

namespace Makku.Shelters.Application.Commands.DeleteShelter
{
    internal class DeleteShelterProfileCommandValidator:AbstractValidator<DeleteShelterProfileCommand>
    {
        public DeleteShelterProfileCommandValidator()
        {
            RuleFor(deleteShelterCommand => deleteShelterCommand.UserId).NotEqual(Guid.Empty).NotNull();
            RuleFor(deleteShelterCommand => deleteShelterCommand.Id).NotEqual(Guid.Empty).NotNull();
        }
    }
}
