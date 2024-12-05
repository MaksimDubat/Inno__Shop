using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Handlers
{
    /// <summary>
    /// Обработчик команды для отправки Email.
    /// </summary>
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly EmailSender _emailSender;
        public SendEmailCommandHandler(EmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _emailSender.SendEmailAsync(request.Email, request.Subject, request.Message);
                return true;    
            }
            catch
            {
                return false;
            }   
        }
    }
}
