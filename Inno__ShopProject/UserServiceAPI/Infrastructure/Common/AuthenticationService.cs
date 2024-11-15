using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserServiceAPI.DataBaseAccess;
using UserServiceAPI.Entities;
using UserServiceAPI.Interface;

namespace UserServiceAPI.Infrastructure.Common
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        /// <inheritdoc/>
        public AuthenticationService(
            SignInManager<AppUsers> signInManager,
            UserManager<AppUsers> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <inheritdoc/>
        public Task<IdentityResult> RegisterAsync(string name, string email, string password, CancellationToken cancellation)
        {
            var user = new AppUsers
            {
                UserName = email,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedDate = DateTime.UtcNow   
            };
            return _userManager.CreateAsync(user);
        }
        /// <inheritdoc/>
        public async Task<bool> SignInAsync(string email, string password, CancellationToken cancellation)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("error");
            }
            var isPasswordMatch = await _userManager.CheckPasswordAsync(user, password);
            if (isPasswordMatch)
            {
                await _signInManager.SignInAsync(user, isPersistent : true);
                return true;    
            }
            else
            {
                return false;
            }
        }
        /// <inheritdoc/>
        public Task SignOutAsync(CancellationToken cancellation)
        {
            return _signInManager.SignOutAsync();
        }
    }
}
