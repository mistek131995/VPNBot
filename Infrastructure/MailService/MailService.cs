using Core.Common;
using MailKit.Net.Smtp;
using MimeKit;

namespace Infrastructure.MailService
{
    public class MailService
    {
        public MailService(IRepositoryProvider repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider;
        }

        private readonly IRepositoryProvider repositoryProvider;

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", settings.SMTPLogin));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(settings.SMTPServer, settings.SMTPPort, false);
                await client.AuthenticateAsync(settings.SMTPLogin, settings.SMTPPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
