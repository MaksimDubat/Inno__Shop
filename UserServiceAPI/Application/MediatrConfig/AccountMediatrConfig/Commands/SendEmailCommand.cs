using MediatR;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands
{
    /// <summary>
    /// Модель команды отправки Email.
    /// </summary>
    public class SendEmailCommand : IRequest<bool>
    {
        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Тема письма.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Сообщение письма.
        /// </summary>
        public string Message { get; set; }

        public SendEmailCommand(string email, string subject, string message)
        {
            Email = email;
            Subject = subject;
            Message = message;
        }
    }
}
