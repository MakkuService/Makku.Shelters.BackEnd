using Makku.Shelters.Application.Shelters.Identity;
using Makku.Shelters.Application.Shelters.Identity.Commands.DeleteShelter;
using Makku.Shelters.Application.Shelters.Identity.Commands.LoginShelter;
using Makku.Shelters.Application.Shelters.Identity.Commands.LogoutShelter;
using Makku.Shelters.Application.Shelters.Identity.Commands.RegisterShelter;
using Makku.Shelters.Application.Shelters.Identity.Commands.ResetPassword;
using Makku.Shelters.Application.Shelters.Identity.Queries.GetCurrentShelter;
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
        /// <returns>OperationResult&lt;CurrentIdentityShelterVm&gt;</returns>
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
            //todo Возврат CurrentIdentityShelterVm
            var command = Mapper.Map<LoginShelterCommand>(login);
            var result = await Mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            await Mediator.Send(new LogoutShelterCommand(), cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteById(Guid identityShelterId, CancellationToken token)
        {
            var requestorGuid = HttpContext.GetIdentityIdClaimValue();

            var command = new DeleteShelterCommand
            {
                IdentityShelterId = identityShelterId,
                RequestorGuid = requestorGuid
            };

            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteCurrentShelter")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteCurrentShelter(CancellationToken token)
        {
            var identityShelterId = HttpContext.GetIdentityIdClaimValue();

            var requestorGuid = HttpContext.GetIdentityIdClaimValue();

            var command = new DeleteShelterCommand
            {
                IdentityShelterId = identityShelterId,
                RequestorGuid = requestorGuid
            };

            await Mediator.Send(command, token);

            return NoContent();
        }

        [HttpGet]
        [Route("CurrentShelter")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CurrentUser(CancellationToken token)
        {
            var userProfileId = HttpContext.GetShelterProfileIdClaimValue();

            var query = new GetCurrentShelterQuery { ShelterProfileId = userProfileId, ClaimsPrincipal = HttpContext.User };
            var result = await Mediator.Send(query, token);

            return Ok(Mapper.Map<CurrentIdentityShelterVm>(result));
        }

        [HttpPost]
        [Route("ResetPassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPaePasswordDto)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            var command = new ResetPasswordCommand
            {
                Email = resetPaePasswordDto.Email,
                Password = resetPaePasswordDto.Password,
                Token = token.Replace("Bearer ","")
            };

            await Mediator.Send(command);
            return NoContent();
        }

    }
}