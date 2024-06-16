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
            var user = await repositoryProvider.UserRepository.GetByEmailAsync(toEmail);

            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", settings.SMTPLogin));
            emailMessage.To.Add(new MailboxAddress(user?.Login ?? "Пользователь", toEmail));
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

        public async Task SendEmailAsync(List<string> toEmails, string subject, string message)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();
            var users = await repositoryProvider.UserRepository.GetByEmailsAsync(toEmails);

            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", settings.SMTPLogin));

            foreach(var toEmail in toEmails)
            {
                var userName = users.FirstOrDefault(x => x.Email == toEmail)?.Login ?? "";
                emailMessage.To.Add(new MailboxAddress(userName, toEmail));
            }

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
