using System.Security.Claims;

namespace Makku.Shelters.WebApi.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserProfileIdClaimValue(this HttpContext context)
        {
            return GetGuidClaimValue("ShelterProfileId", context);
        }

        public static Guid GetIdentityIdClaimValue(this HttpContext context)
        {
            return GetGuidClaimValue("IdentityId", context);
        }

        private static Guid GetGuidClaimValue(string key, HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            var value = identity?.FindFirst(key)?.Value;

            if (value != null) 
                return Guid.Parse(value);
            return Guid.Empty;
        }
    }
}
