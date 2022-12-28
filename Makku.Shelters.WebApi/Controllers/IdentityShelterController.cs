using Makku.Shelters.Application.IdentityShelter.Commands.DeleteShelter;
using Makku.Shelters.Application.IdentityShelter.Commands.LoginShelter;
using Makku.Shelters.Application.IdentityShelter.Commands.RegisterShelter;
using Makku.Shelters.Application.IdentityShelter.Dtos;
using Makku.Shelters.Application.IdentityShelter.Queries;
using Makku.Shelters.WebApi.Extensions;
using Makku.Shelters.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makku.Shelters.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class IdentityShelterController : BaseController
    {
        /// <summary>
        /// Register the shelter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /shelter
        ///{
        ///    "username": "testUserName1",
        ///    "email": "testUserName1@test.com",
        ///    "password": "testUserName!_1",
        ///    "shelterName": "Test shelter name"
        ///}
        /// </remarks>
        /// <param name="registerShelterDto">registerShelterDto object</param>
        /// <returns>OperationResult&lt;IdentityShelterProfileVm&gt;</returns>
        /// <response code="200">Success</response>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterShelterDto registerShelterDto, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<RegisterShelterCommand>(registerShelterDto);
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginShelterDto login, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<LoginShelterCommand>(login);
            var result = await Mediator.Send(command, cancellationToken);

            return result.IsError 
                ? HandleErrorResponse(result.Errors) 
                : Ok(Mapper.Map<IdentityShelterProfileVm>(result.Payload));
        }


        [HttpDelete]
        [Route("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(string identityShelterId, CancellationToken token)
        {
            //todo Обработка ошибок
            var identityShelterGuid = Guid.Parse(identityShelterId);
            var requestorGuid = HttpContext.GetIdentityIdClaimValue();
            var command = new DeleteShelterCommand
            {
                IdentityShelterId = identityShelterGuid,
                RequestorGuid = requestorGuid
            };
            var result = await Mediator.Send(command, token);

            return result.IsError 
                ? HandleErrorResponse(result.Errors) 
                : NoContent();
        }

        [HttpGet]
        [Route("CurrentShelter")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CurrentUser(CancellationToken token)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var query = new GetCurrentShelterQuery { ShelterProfileId = userProfileId, ClaimsPrincipal = HttpContext.User };
            var result = await Mediator.Send(query, token);

            return result.IsError 
                ? HandleErrorResponse(result.Errors) 
                : Ok(Mapper.Map<IdentityShelterProfileVm>(result.Payload));
        }

    }
}