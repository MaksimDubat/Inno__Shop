﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UserServiceAPI.Application.EmailSet.EmaiGeneration;


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
    /// <summary>
    /// Отправка Email пользователю.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
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