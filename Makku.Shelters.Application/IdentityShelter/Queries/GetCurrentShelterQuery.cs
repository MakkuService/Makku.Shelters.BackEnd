using Makku.Shelters.Application.Models;
using MediatR;
using System.Security.Claims;
using Makku.Shelters.Application.IdentityShelter.Dtos;

namespace Makku.Shelters.Application.IdentityShelter.Queries
{
    public class GetCurrentShelterQuery : IRequest<OperationResult<IdentityShelterProfileVm>>
    {
        public Guid ShelterProfileId { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
