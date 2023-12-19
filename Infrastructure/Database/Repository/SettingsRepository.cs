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
                    SSHServerIP = x.SSHServerIP,
                    SSHServerLogin = x.SSHServerLogin,
                    SSHServerPassword = x.SSHServerPassword,
                    FileBasePath = x.FileBasePath,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
