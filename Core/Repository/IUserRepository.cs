using Core.Model.User;

namespace Core.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(int id);
        public Task<List<User>> GetByIdsAsync(List<int> ids);
        public Task<List<User>> GetByParentIdAsync(int parentId);
        public Task<User> GetByTelegramUserIdAsync(long telegramUserId);
        public Task<User> GetByLoginAsync(string login);
        public Task<User> GetByEmailAsync(string email);
        public Task<User> GetByLoginAndPasswordAsync(string login, string password);
        public Task<User> GetByGuidAsync(Guid guid);
        public Task<User> AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task UpdateManyAsync(List<User> users);
    }
}
