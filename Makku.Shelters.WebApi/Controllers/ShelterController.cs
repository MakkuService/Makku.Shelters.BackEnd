using AutoMapper;
using Makku.Shelters.Application.Commands.CreateShelter;
using Makku.Shelters.Application.Commands.DeleteShelter;
using Makku.Shelters.Application.Commands.UpdateShelter;
using Makku.Shelters.Application.Queries.GetShelterDetails;
using Makku.Shelters.Application.Queries.GetShelterList;
using Makku.Shelters.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Makku.Shelters.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ShelterController : BaseController
    {
        private readonly IMapper _mapper;

        public ShelterController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<ShelterListVm>> GetAll()
        {
            var query = new GetShelterListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateShelterDto createShelterDto)
        {
            var command = _mapper.Map<CreateShelterCommand>(createShelterDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateShelterDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateShelterCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteShelterCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}