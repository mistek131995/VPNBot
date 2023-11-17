using Database.Model;

namespace Database.Repository.Interface
{
    public interface IAccessRepository
    {
        public Task<Access> GetByTelegramUserIdAsync(long tlegramUserId);
    }
}
