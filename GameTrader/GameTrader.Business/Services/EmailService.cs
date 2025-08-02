using GameTrader.Core.Helpers;
using GameTrader.Core.Interfaces.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPHelper _smtpHelper;

        public EmailService(SMTPHelper smtpHelper)
        {
            _smtpHelper = smtpHelper;
        }

        public async Task EmailSender(string to, string subject, string content)
        {           
            var mailfrom = _smtpHelper.GetSmtpUsername();           
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_smtpHelper.GetSmtpUsername()));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("html")
            {
                Text = content
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpHelper.GetSmtpServer(), int.Parse(_smtpHelper.GetSmtpPort()), SecureSocketOptions.SslOnConnect);
            await smtp.AuthenticateAsync(_smtpHelper.GetSmtpUsername(), (_smtpHelper.GetSmtpPassword()));
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
