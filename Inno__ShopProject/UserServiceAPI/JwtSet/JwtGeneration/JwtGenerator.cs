using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserServiceAPI.Entities;

namespace UserServiceAPI.JwtSet.JwtGeneration
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtGenerator(IOptions<JwtOptions> jwtOptions) 
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <inheritdoc/>
        public string GenerateToken(AppUsers user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (ClaimTypes.Name, user.Name),
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));    
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims:claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
