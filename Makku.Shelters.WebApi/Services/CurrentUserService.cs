using System.Security.Claims;
using Makku.Shelters.Application.Interfaces;

namespace Makku.Shelters.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService() { }

        public Guid UserId
        {
            get
            {
                var id = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }
    }
}
