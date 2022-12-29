using MediatR;
using System.Security.Claims;

namespace Makku.Shelters.Application.Shelters.Identity.Queries.GetCurrentShelter
{
    public class GetCurrentShelterQuery : IRequest<CurrentIdentityShelterVm>
    {
        public Guid ShelterProfileId { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
