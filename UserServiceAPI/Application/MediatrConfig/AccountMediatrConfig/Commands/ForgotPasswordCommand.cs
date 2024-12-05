using MediatR;
using UserServiceAPI.Application.Models.PasswordResetModels;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands
{
    /// <summary>
    /// Модель команды сброса пароля.
    /// </summary>
    public class ForgotPasswordCommand : IRequest<bool>
    {
        /// <summary>
        /// Модель сброса пароля.
        /// </summary>
        public PasswordResetModel Model { get; set; }
        public ForgotPasswordCommand(PasswordResetModel model)
        {
            Model = model;
        }
    }
}
