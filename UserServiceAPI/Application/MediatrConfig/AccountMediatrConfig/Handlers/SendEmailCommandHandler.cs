using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Commands;
using UserServiceAPI.Domain.Entities;
using UserServiceAPI.Infrastructure.DataBase;

namespace UserServiceAPI.Application.MediatrConfig.AccountMediatrConfig.Handlers
{
    /// <summary>
    /// Обработчик команды для отправки Email.
    /// </summary>
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly EmailSender _emailSender;
        private readonly MutableInnoShopDbContext _innoShopDbContext;
        public SendEmailCommandHandler(EmailSender emailSender, MutableInnoShopDbContext innoShopDbContext)
        {
            _emailSender = emailSender;
            _innoShopDbContext = innoShopDbContext;
        }
        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var code = GenerateRandomeCode();
            var existCode =  _innoShopDbContext.ConfirmCodes.FirstOrDefault(x => x.Email == request.Email);   
            if (existCode != null)
            {
                _innoShopDbContext.ConfirmCodes.Remove(existCode);
            }
            var confirm = new ConfirmCode
            {
                Email = request.Email,
                Code = code,
                ExpiryDate = DateTime.UtcNow.AddMinutes(10)
            };
            _innoShopDbContext.ConfirmCodes.Add(confirm);
            await _innoShopDbContext.SaveChangesAsync(cancellationToken);

            var subject = request.Subject ?? "Code";
            var mesaage = request.Message ?? $"code: {code}";

            try
            {
                await _emailSender.SendEmailAsync(request.Email, subject, mesaage);
                return true;
            }
            catch
            {
                _innoShopDbContext.ConfirmCodes.Remove(confirm);
                await _innoShopDbContext.SaveChangesAsync(cancellationToken);
                return false;
            }
        }
        private static string GenerateRandomeCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
