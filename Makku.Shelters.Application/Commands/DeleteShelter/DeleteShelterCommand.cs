using MediatR;

namespace Makku.Shelters.Application.Commands.DeleteShelter
{
    public class DeleteShelterCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
