using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserServiceAPI.DataBaseAccess;
using UserServiceAPI.Entities;
using UserServiceAPI.Interface;
using UserServiceAPI.JwtSet.JwtGeneration;

namespace UserServiceAPI.Infrastructure.Common
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        /// <inheritdoc/>
        public AuthenticationService(
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager,
            IJwtGenerator jwtGenerator,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>
        public async Task<IdentityResult> RegisterAsync(string email, string name, string password, CancellationToken cancellation)
        {
            var user = new AppUsers
            {
                Email = email,
                UserName = email,
                Name = name,
                PasswordHash = password,
                CreatedDate = DateTime.UtcNow,
                Role = UserRole.User  
            };
           return await _userManager.CreateAsync(user, password);
         
        }
        /// <inheritdoc/>
        public async Task<string> SignInAsync(string email, string password, CancellationToken cancellation)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password ))
            {
                throw new UnauthorizedAccessException("error");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtGenerator.GenerateToken(user, roles);

            var dbContext = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<MutableInnoShopDbContext>();
            var userToken = new LoginResponse
            {
                UserId = user.UserId.ToString(),
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            };

            dbContext.Add(userToken);   
            await dbContext.SaveChangesAsync(cancellation);
            return token;
        }
        /// <inheritdoc/>
        public Task SignOutAsync(CancellationToken cancellation)
        {
            return _signInManager.SignOutAsync();
        }
    }
}
