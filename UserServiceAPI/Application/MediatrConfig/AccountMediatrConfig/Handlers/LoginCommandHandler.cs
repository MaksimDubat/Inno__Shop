using MediatR;
using Microsoft.AspNetCore.Identity;
using UserServiceAPI.Application.JwtSet.JwtGeneration;
using UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Handlers
{
    /// <summary>
    /// Обработчик команды для взода пользователя.
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _authenticationService.SignInAsync(request.Email, request.Password, cancellationToken);
                return token;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("invalid", ex);
            }
        }
    }
}
