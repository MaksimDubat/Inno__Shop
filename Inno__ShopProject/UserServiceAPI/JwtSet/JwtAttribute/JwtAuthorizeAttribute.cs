using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace UserServiceAPI.JwtSet.JwtAttribute
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public JwtAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
