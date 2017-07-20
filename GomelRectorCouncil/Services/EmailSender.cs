using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using GomelRectorCouncil.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace GomelRectorCouncil.Services
{
    // Этот класс используется приложением для отправки электронной почты

    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig ec;

        public EmailSender(IOptions<EmailConfig> emailConfig)
        {
            ec = emailConfig.Value;
        }
        public async Task SendEmailAsync(String email, String subject, String message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Clear();
                emailMessage.From.Add(new MailboxAddress(ec.FromName, ec.FromAddress));

                emailMessage.To.Clear();
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = message };

                using (var client = new SmtpClient())
                {
                    client.LocalDomain = ec.LocalDomain;

                    await client.ConnectAsync(ec.MailServerAddress, Convert.ToInt32(ec.MailServerPort), SecureSocketOptions.Auto).ConfigureAwait(false);
                    await client.AuthenticateAsync(new NetworkCredential(ec.UserId, ec.UserPassword));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
