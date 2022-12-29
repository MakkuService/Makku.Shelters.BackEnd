using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Makku.Shelters.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
    }
}
