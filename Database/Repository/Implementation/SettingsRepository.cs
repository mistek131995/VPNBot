﻿using Database.Model;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implementation
{
    internal class SettingsRepository(Context context) : ISettingsRepository
    {
        public Context context = context;


        public async Task<Setting> GetSettingsAsync()
        {
            return await context.Settings.FirstOrDefaultAsync();
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
                await context.Settings.AddAsync(newSetting);

                return newSetting;
            }

            return settings;
        }
    }
}
