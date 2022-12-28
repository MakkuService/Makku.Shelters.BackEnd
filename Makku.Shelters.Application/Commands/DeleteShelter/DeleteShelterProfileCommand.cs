using MediatR;

namespace Makku.Shelters.Application.Commands.DeleteShelter
{
    public class DeleteShelterProfileCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
