using Destructurama.Attributed;
using MediatR;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.RegisterShelter
{
    public class RegisterShelterCommand : IRequest<CurrentIdentityShelterVm>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        [NotLogged]
        public string Password { get; set; }

        public string ShelterName { get; set; }
    }
}
