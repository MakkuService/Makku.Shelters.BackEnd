using Destructurama.Attributed;
using MediatR;
using System.Security.Claims;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.LogoutShelter
{
    public class LogoutShelterCommand : IRequest
    {
        public Guid ShelterProfileId { get; set; }

        [NotLogged]
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

    }
}
