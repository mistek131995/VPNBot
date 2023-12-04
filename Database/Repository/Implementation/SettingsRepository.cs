using Database.Model;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implementation
{
    internal class SettingsRepository(Context context) : ISettingsRepository
    {
        public async Task<Setting> GetSettingsAsync()
        {
            return await context.Settings
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Setting> UpdateSettingsAsync(Setting newSetting)
        {
            var settings = await context.Settings.FirstOrDefaultAsync();

            if (settings != null)
            {
                settings.TelegramToken = newSetting.TelegramToken;
            }
            else
            {
                settings = newSetting;
                await context.Settings.AddAsync(settings);
            }

            await context.SaveChangesAsync();

            return settings;
        }
    }
}
