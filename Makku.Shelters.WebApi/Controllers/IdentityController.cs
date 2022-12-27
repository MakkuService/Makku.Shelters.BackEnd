using Makku.Shelters.Application.Identity.Commands.LoginShelter;
using Makku.Shelters.Application.Identity.Commands.RegisterShelter;
using Makku.Shelters.Application.Identity.Dtos;
using Makku.Shelters.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Makku.Shelters.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class IdentityController : BaseController
    {
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register([FromBody] RegisterShelterDto registerShelterDto, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<RegisterShelterCommand>(registerShelterDto);
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginShelterDto login, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<LoginShelterCommand>(login);
            var result = await Mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(Mapper.Map<IdentityShelterProfileDto>(result.Payload));
        }
    }
}