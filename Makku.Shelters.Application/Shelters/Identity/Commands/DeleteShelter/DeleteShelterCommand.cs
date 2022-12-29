using MediatR;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.DeleteShelter
{
    public class DeleteShelterCommand : IRequest
    {
        public Guid IdentityShelterId { get; set; }
        public Guid RequestorGuid { get; set; }
    }
}
