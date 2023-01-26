using Destructurama.Attributed;
using MediatR;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.LoginShelter
{
    public class LoginShelterCommand : IRequest<CurrentIdentityShelterVm>
    {
        public string Email { get; set; }

        [NotLogged]
        public string Password { get; set; }
    }
}
