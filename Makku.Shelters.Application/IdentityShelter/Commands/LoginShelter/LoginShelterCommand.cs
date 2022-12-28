using Makku.Shelters.Application.IdentityShelter.Dtos;
using Makku.Shelters.Application.Models;
using MediatR;

namespace Makku.Shelters.Application.IdentityShelter.Commands.LoginShelter
{
    public class LoginShelterCommand : IRequest<OperationResult<IdentityShelterProfileVm>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
