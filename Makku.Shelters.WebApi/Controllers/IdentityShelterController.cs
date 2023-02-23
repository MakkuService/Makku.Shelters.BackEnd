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
        /// 
        ///     POST /register
        ///     {
        ///         "username": "testUserName1",
        ///         "email": "user@example.com",
        ///         "password": "jkd!21Sdlkj",
        ///         "shelterName": "Test shelter name"
        ///     }
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

        /// <summary>
        /// Login as shelter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /login
        ///     {
        ///         "email": "user@example.com",
        ///         "password": "jkd!21Sdlkj"
        ///     }
        /// </remarks>
        /// <param name="login">LoginShelterDto object</param>
        /// <returns>OperationResult&lt;CurrentIdentityShelterVm&gt;</returns>
        /// <response code="200">Success</response>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginShelterDto login, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<LoginShelterCommand>(login);
            var result = await Mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Logout from shelter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /logout
        /// </remarks>
        /// <response code="200">Success</response>
        [HttpPost]
        [Route("Logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetShelterProfileIdClaimValue();

            var query = new LogoutShelterCommand { ShelterProfileId = userProfileId, ClaimsPrincipal = HttpContext.User };

            await Mediator.Send(query, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Delete shelter by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /deletebyid
        ///     {
        ///         "email": "user@example.com",
        ///         "password": "jkd!21Sdlkj"
        ///     }
        /// </remarks>
        /// <param name="identityShelterId">Shelter id</param>
        /// <response code="200">Success</response>
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

        /// <summary>
        /// Delete current shelter
        /// </summary>
        /// <remarks>
        ///     POST /deletecurrentshelter
        /// </remarks>
        /// <response code="200">Success</response>
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

        /// <summary>
        /// Get info from current shelter
        /// </summary>
        /// <remarks>
        ///     GET /currentshelter
        /// </remarks>
        /// <response code="200">Success</response>

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

        //todo ѕока не используем, потом вернутьс€ к этому методу
        //[HttpPost]
        //[Route("ResetPassword")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPaePasswordDto)
        //{
        //    var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        //    var command = new ResetPasswordCommand
        //    {
        //        Email = resetPaePasswordDto.Email,
        //        Password = resetPaePasswordDto.Password,
        //        Token = token.Replace("Bearer ","")
        //    };

        //    await Mediator.Send(command);
        //    return NoContent();
        //}

    }
}