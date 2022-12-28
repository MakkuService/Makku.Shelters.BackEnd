using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.WebApi.Extensions;

namespace Makku.Shelters.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;
        public Guid UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.GetIdentityIdClaimValue();
                return id ?? Guid.Empty;
            }
        }
    }
}
