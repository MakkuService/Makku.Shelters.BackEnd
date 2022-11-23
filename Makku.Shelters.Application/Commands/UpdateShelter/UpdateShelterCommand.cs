using MediatR;

namespace Makku.Shelters.Application.Commands.UpdateShelter
{
    public class UpdateShelterCommand:IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }

    }
}
