using FluentValidation;

namespace Makku.Shelters.Application.Commands.CreateShelter
{
    internal class CreateShelterCommandValidator : AbstractValidator<CreateShelterCommand>
    {
        public CreateShelterCommandValidator()
        {
            RuleFor(createShelterCommand => createShelterCommand.Name).NotEmpty().MaximumLength(100);
            RuleFor(createShelterCommand => createShelterCommand.UserId).NotEqual(Guid.Empty).NotNull();
        }
    }
}
