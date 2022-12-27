using Makku.Shelters.Application.Identity.Dtos;
using Makku.Shelters.Application.Models;
using MediatR;

namespace Makku.Shelters.Application.Identity.Commands.RegisterShelter
{
    public class RegisterShelterCommand : IRequest<OperationResult<IdentityShelterProfileDto>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ShelterName { get; set; }
    }
}
