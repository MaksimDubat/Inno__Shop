using MediatR;
using Microsoft.AspNetCore.Identity;
using UserServiceAPI.Application.JwtSet.JwtGeneration;
using UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands;
using UserServiceAPI.Domain.Entities;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Handlers
{
    /// <summary>
    /// Обработчик команды для взода пользователя.
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginCommandHandler(UserManager<AppUsers> userManager, SignInManager<AppUsers> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Email)
                    ?? throw new UnauthorizedAccessException();
            if(!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException();
            }
            await _signInManager.SignInAsync(user, isPersistent: true);
            var roles  = await _userManager.GetRolesAsync(user);
            return _jwtGenerator.GenerateToken(user, roles);
        }
    }
}
