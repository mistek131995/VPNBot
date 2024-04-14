using Core.Model.Settings;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class SettingsRepository(Context context) : ISettingsRepositroy
    {
        public async Task<Settings> GetSettingsAsync()
        {
            return await context.Settings
                .Select(x => new Settings
                {
                    Id = x.Id,
                    TelegramToken = x.TelegramToken,

                    CaptchaPrivateKey = x.CaptchaPrivateKey,
                    CaptchaPublicKey = x.CaptchaPublicKey,

                    SMTPServer = x.SMTPServer,
                    SMTPPort = x.SMTPPort,
                    SMTPLogin = x.SMTPLogin,
                    SMTPPassword = x.SMTPPassword,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
