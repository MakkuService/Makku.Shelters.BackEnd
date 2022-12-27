using Makku.Shelters.Application.Identity.Dtos;
using Makku.Shelters.Application.Models;
using MediatR;

namespace Makku.Shelters.Application.Identity.Commands.LoginShelter
{
    public class LoginShelterCommand : IRequest<OperationResult<IdentityShelterProfileDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
