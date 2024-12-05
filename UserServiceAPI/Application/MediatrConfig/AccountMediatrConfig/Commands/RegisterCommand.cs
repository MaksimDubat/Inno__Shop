using MediatR;
using UserServiceAPI.Application.Models.RegistartionModels;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands
{
    /// <summary>
    /// Модель команды регистрации пользователя.
    /// </summary>
    public class RegisterCommand : IRequest<RegistrationModel>
    {
        /// <summary>
        /// Модель регистрации.
        /// </summary>
        public RegistrationModel Model { get; set; }

        public RegisterCommand(RegistrationModel model)
        {
            Model = model;
        }   
    }
}
