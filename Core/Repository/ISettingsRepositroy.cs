using Core.Model.Settings;

namespace Core.Repository
{
    public interface ISettingsRepositroy
    {
        public Task<Settings> GetSettingsAsync();
    }
}
