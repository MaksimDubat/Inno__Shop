using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductServiceAPI.HttpExtensions
{
    /// <summary>
    /// Класс расширения HttpContext для работы с токенами.
    /// </summary>
    public static class HttpContextExtension
    {
        public static int? GetUserId(this HttpContext httpContext)
        {
            if (httpContext?.User?.Identity is ClaimsIdentity identity)
            {
                var userIdClaim = identity.FindFirst(JwtRegisteredClaimNames.Sub) ??
                                  identity.FindFirst("userId") ??
                                  identity.FindFirst("id");

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                {
                    return userId;
                }
            }
            return null;
        }
    }
}
