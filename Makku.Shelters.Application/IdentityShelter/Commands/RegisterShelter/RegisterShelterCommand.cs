using Makku.Shelters.Application.IdentityShelter.Dtos;
using Makku.Shelters.Application.Models;
using MediatR;

namespace Makku.Shelters.Application.IdentityShelter.Commands.RegisterShelter
{
    public class RegisterShelterCommand : IRequest<OperationResult<IdentityShelterProfileVm>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ShelterName { get; set; }
    }
}
