using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using System.Text;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace eLab.Repositories
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailsettings)
        {
            _emailSettings = new EmailSettings();
            _emailSettings.EnableSsl = true;
            _emailSettings.MailPort = 587;
            _emailSettings.MailServer = "smtp.gmail.com";
            _emailSettings.SenderName = "eLab";
            _emailSettings.UseDefaultCredentials = false;
            _emailSettings.SenderEmail = "elab024@gmail.com";
            _emailSettings.SenderPassword = "labtest123...N";
        }
        public Task SendEmail(string email, string subject, string htmlMessage)
        {
            try
            {
                using (var client = new SmtpClient
                {
                    Host = _emailSettings.MailServer,
                    Port = _emailSettings.MailPort,
                    EnableSsl = _emailSettings.EnableSsl,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword)
                })
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName)
                    };

                    mailMessage.To.Add(email);
                    mailMessage.Subject = subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = htmlMessage;
                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Task.FromResult(0);
        }


        public class EmailSettings
        {
            public string MailServer { get; set; }
            public int MailPort { get; set; }
            public bool EnableSsl { get; set; }
            public string SenderName { get; set; }
            public string SenderEmail { get; set; }
            public string SenderPassword { get; set; }
            public bool UseDefaultCredentials { get; set; }
        }

    }
}
