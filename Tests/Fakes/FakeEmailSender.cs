using SSBO5G__Szakdolgozat.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Fakes
{
    class FakeEmailSender : IEmailSender
    {
        public class FakeEmail
        {
            public string Address { get; set; }
            public string Text { get; set; }
            public string Subject { get; set; }
            public byte[] PdfFile { get; set; }
            public string FileName { get; set; }
        }

        public FakeEmail Email { get; set; }
        public async Task SendEmail(string address, string text, string subject, byte[] pdfFile = null, string fileName = null)
        {
            this.Email = new FakeEmail
            {
                Address = address,
                Text = text,
                Subject = subject,
                PdfFile = pdfFile,
                FileName = fileName
            };
        }
    }
}
