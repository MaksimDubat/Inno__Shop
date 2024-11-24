using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProductServiceAPI.HttpExtensions
{
    public static class HttpContextExtension
    {
        public static int? GetUserId(this HttpContext httpcontext)
        {
            if(httpcontext.User.Identities is ClaimsIdentity identity)
            {
                var userIdClaim = identity.FindFirst(JwtRegisteredClaimNames.Sub);
                if(userIdClaim != null)
                {
                    if(int.TryParse(userIdClaim.Value, out var userId))
                    {
                        return userId;
                    }
                }
            }
            return null;
        }
    }
}
