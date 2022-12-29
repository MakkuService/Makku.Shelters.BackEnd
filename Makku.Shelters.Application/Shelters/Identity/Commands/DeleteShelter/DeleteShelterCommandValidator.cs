using FluentValidation;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.DeleteShelter
{
    public class DeleteShelterCommandValidator : AbstractValidator<DeleteShelterCommand>
    {
        public DeleteShelterCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.RequestorGuid).NotEqual(Guid.Empty);
            RuleFor(deleteNoteCommand => deleteNoteCommand.IdentityShelterId).NotEqual(Guid.Empty);
        }
    }
}
