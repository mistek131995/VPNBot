using Database.Model;

namespace Database.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(int id);
        public Task<User> GetByTelegramUserIdAsync(long telegramUserId);
        public Task<User> GetByTelegramUserIdAndAccessGuidAsync(long telegramUserId, Guid accessGuid);
        public Task<User> AddAsync(User user);

        /// <summary>
        /// Обновляет пользователя со всеми вложенными сущностями
        /// </summary>
        /// <param name="user">Пользователь и вложенные сущности</param>
        /// <returns></returns>
        public Task<User> UpdateAsync(User user);
    }
}
