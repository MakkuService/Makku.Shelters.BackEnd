using FluentValidation;

namespace Makku.Shelters.Application.Commands.DeleteShelter
{
    internal class DeleteShelterCommandValidator:AbstractValidator<DeleteShelterCommand>
    {
        public DeleteShelterCommandValidator()
        {
            RuleFor(deleteShelterCommand => deleteShelterCommand.UserId).NotEqual(Guid.Empty).NotNull();
            RuleFor(deleteShelterCommand => deleteShelterCommand.Id).NotEqual(Guid.Empty).NotNull();
        }
    }
}
