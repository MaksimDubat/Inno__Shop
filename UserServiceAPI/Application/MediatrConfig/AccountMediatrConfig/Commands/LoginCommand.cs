using MediatR;
using UserServiceAPI.Application.Models.LoginModels;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands
{
    /// <summary>
    /// Модель команды для осуществления входа пользователя.
    /// </summary>
    public class LoginCommand : IRequest<string>
    {
        /// <summary>
        /// Модель входа.
        /// </summary>
        public LoginModel Model { get; set; }

        public LoginCommand(LoginModel model)
        {
            Model = model;
        }   
    }
}
