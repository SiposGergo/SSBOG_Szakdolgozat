using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSBO5G__Szakdolgozat.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string address, string text, string subject);
    }
    public class EmailSender : IEmailSender
    {
        string passWord;
        string userName;
        public EmailSender(string userName, string passWord)
        {
            this.userName = userName;
            this.passWord = passWord;
        }
        public async Task SendEmail(string address, string text, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HikeX Rendszer", "hikex.system@gmail.com"));
            message.To.Add(new MailboxAddress(address));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(userName,passWord);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}

