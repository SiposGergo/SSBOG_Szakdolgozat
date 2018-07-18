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
        Task SendEmail(string address, string text, string subject, byte[] pdfFile = null, string fileName = null);
    }
    public class EmailSender : IEmailSender
    {
        private readonly string passWord;
        private readonly string userName;
        public EmailSender(string userName, string passWord)
        {
            this.userName = userName;
            this.passWord = passWord;
        }
        public async Task SendEmail(string address, string text, string subject, byte[] pdfFile = null, string fileName = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("HikeX Rendszer", "hikex.system@gmail.com"));
            message.To.Add(new MailboxAddress(address));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.TextBody = text;
            if (pdfFile != null)
            {
                builder.Attachments.Add(fileName ?? "attachment.pdf", pdfFile);
            }
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(userName, passWord);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}

