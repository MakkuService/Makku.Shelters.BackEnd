using MediatR;

namespace Makku.Shelters.Application.Commands.CreateShelter
{
    public class CreateShelterCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }

    }
}
