using MediatR;
using Microsoft.AspNetCore.Identity;
using UserServiceAPI.Application.JwtSet.JwtGeneration;
using UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands;
using UserServiceAPI.Application.Models.RegistartionModels;
using UserServiceAPI.Application.Services.Common;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Domain.Interface;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Handlers
{
    /// <summary>
    /// Обработчик команды регистрации пользователя.
    /// </summary>
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegistrationModel>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        async Task<RegistrationModel> IRequestHandler<RegisterCommand, RegistrationModel>.Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            if (model.Password != model.ConfirmPassword)
            {
                model.Errors = new List<string>();
                return model;
            }
            var result = await _authenticationService.RegisterAsync(
               request.Model.Email,
               request.Model.Name, 
               request.Model.Password,
               cancellationToken);

            if (!result.Succeeded)
            {
                model.Errors = result.Errors.Select(e => e.Description).ToList();   
                return model;
            }
            model.Errors = null;
            return model;
        }
    }
}
