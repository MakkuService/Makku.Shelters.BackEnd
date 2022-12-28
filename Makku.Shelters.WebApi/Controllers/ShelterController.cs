using AutoMapper;
using Makku.Shelters.Application.Commands.CreateShelter;
using Makku.Shelters.Application.Commands.DeleteShelter;
using Makku.Shelters.Application.Commands.UpdateShelter;
using Makku.Shelters.Application.Queries.GetShelterDetails;
using Makku.Shelters.Application.Queries.GetShelterList;
using Makku.Shelters.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Makku.Shelters.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShelterController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;


        public ShelterController(IMapper mapper, UserManager<IdentityUser> userManager)
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
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ShelterListVm>> GetAll()
        {
            var cur = ClaimsPrincipal.Current;
            var userId = _userManager.GetUserId(cur);
            var query = new GetShelterListQuery
            {
                UserId = UserId
            };
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ShelterDetailsVm>> Get(Guid id)
        {
            var query = new GetShelterDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the shelter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /shelter
        /// {
        ///     title: "shelter title",
        ///     details: "shelter details"
        /// }
        /// </remarks>
        /// <param name="createShelterDto">createShelterDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateShelterDto createShelterDto)
        {
            var command = _mapper.Map<CreateShelterCommand>(createShelterDto);
            command.UserId = UserId;
            var shelterID = await Mediator.Send(command);
            return Ok(shelterID);
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
        public async Task<IActionResult> Update([FromBody] UpdateShelterDto updateShelterDto)
        {
            var command = _mapper.Map<UpdateShelterCommand>(updateShelterDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the shelter by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /shelter/88DEB432-062F-43DE-8DCD-8B6EF79073D3
        /// </remarks>
        /// <param name="id">Id of the shelter (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteShelterProfileCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}