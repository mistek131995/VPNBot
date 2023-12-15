using Core.Model.User;

namespace Core.Repository
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllWithActiveAccessAsync();
        public Task<User> GetByIdAsync(int id);
        public Task<List<User>> GetByIdsAsync(List<int> ids);
        public Task<User> GetByTelegramUserIdAsync(long telegramUserId);
        public Task<User> GetByTelegramUserIdAndAccessGuidAsync(long telegramUserId, Guid accessGuid);
        public Task<User> GetByLoginAndPasswordAsync(string login, string password);
        public Task<List<User>> GetByAccessDateRangeAsync(DateTime start, DateTime end);
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task UpdateManyAsync(List<User> users);
    }
}
