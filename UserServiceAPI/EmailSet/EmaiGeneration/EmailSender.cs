using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UserServiceAPI.EmailSet.EmaiGeneration;
using UserServiceAPI.Entities;

/// <summary>
/// Класс реализации отправки Email через SMTP-сервер.
/// </summary>
public class EmailSender : IEmailSender
{
    private readonly EmailOptions _emailOptions;

    public EmailSender(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var smtpClient = new SmtpClient(_emailOptions.SmtpServer)
        {
            Port = _emailOptions.Port,
            Credentials = new NetworkCredential(_emailOptions.From, _emailOptions.Password),
            EnableSsl = true
        };

        var emailMessage = new MailMessage
        {
            From = new MailAddress(_emailOptions.From),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };
        emailMessage.To.Add(email);
        await smtpClient.SendMailAsync(emailMessage);
    }
}