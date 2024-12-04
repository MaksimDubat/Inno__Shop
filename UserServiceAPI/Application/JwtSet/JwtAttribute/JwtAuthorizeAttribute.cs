using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace UserServiceAPI.Application.JwtSet.JwtAttribute
{
    /// <summary>
    /// Расширение стандратного атрибута AuthorizeAttribute для работы с Jwt.
    /// </summary>
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public JwtAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
