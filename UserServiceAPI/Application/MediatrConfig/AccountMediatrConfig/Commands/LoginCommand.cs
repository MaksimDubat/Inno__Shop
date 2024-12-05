using MediatR;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands
{
    /// <summary>
    /// Модель команды для осуществления входа пользователя.
    /// </summary>
    public class LoginCommand : IRequest<string>
    {
        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
