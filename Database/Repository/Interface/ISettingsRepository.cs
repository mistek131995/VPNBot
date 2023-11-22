using Database.Model;

namespace Database.Repository.Interface
{
    internal interface ISettingsRepository
    {
        public Task<Setting> GetSettingsAsync();
        public Task<Setting> UpdateSettingsAsync(Setting newSetting);

    }
}
