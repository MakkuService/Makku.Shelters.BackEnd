using AutoMapper;
using Makku.Shelters.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Makku.Shelters.Application.Shelters.Profile.Commands.UpdateProfile;
using Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterDetails;
using Makku.Shelters.Application.Shelters.Profile.Queries.GetShelterList;
using Makku.Shelters.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Makku.Shelters.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShelterProfileController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;


        public ShelterProfileController(IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets list of shelters
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /shelter
        /// </remarks>
        /// <returns>Returns ShelterListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ShelterListVm>> GetAll()
        {
            var query = new GetShelterListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the shelter by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /shelter/D34D349E-43B8-429E-BCA4-793C932FD580
        /// </remarks>
        /// <param name="id">Shelter id (guid)</param>
        /// <returns>Returns ShelterDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShelterDetailsVm>> Get(Guid id)
        {
            var query = new GetShelterDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Updates the shelter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /shelter
        /// {
        ///     name: "updated shelter name"
        /// }
        /// </remarks>
        /// <param name="updateShelterDto">updateShelterDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] UpdateShelterDto updateShelterDto, CancellationToken cancellationToken)
        {
            var identityIdClaimValue = HttpContext.GetIdentityIdClaimValue();
            var command = _mapper.Map<UpdateProfileCommand>(updateShelterDto);
            command.IdentityShelterId = identityIdClaimValue;
            command.RequestorGuid = identityIdClaimValue;
            await Mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}