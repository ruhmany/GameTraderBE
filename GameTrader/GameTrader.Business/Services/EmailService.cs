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
        public async Task EmailSender(string to, string subject, string content)
        {           
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("nicolas.douglas@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("html")
            {
                Text = content
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("nicolas.douglas@ethereal.email", "rgjBQ4HqJGB7qG1Hyg");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
