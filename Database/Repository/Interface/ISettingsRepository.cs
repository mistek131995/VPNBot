using Database.Model;

namespace Database.Repository.Interface
{
    public interface ISettingsRepository
    {
        public Task<Setting> GetSettingsAsync();
        public Task<Setting> UpdateSettingsAsync(Setting newSetting);

    }
}
