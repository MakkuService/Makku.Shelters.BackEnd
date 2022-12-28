using Makku.Shelters.Application.Models;
using MediatR;

namespace Makku.Shelters.Application.IdentityShelter.Commands.DeleteShelter
{
    public class DeleteShelterCommand : IRequest<OperationResult<bool>>
    {
        public Guid IdentityShelterId { get; set; }
        public Guid RequestorGuid { get; set; }
    }
}
