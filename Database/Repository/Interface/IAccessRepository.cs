using Database.Model;

namespace Database.Repository.Interface
{
    public interface IAccessRepository
    {
        public Task<Access> GetByTelegramUserIdAsync(long tlegramUserId);
        public Task<List<Access>> GetDeprecatedAccessAsync(DateTime deprecatedDate);
        public Task DeleteAccessAsync(long telegramUserId);
        public Task UpdateManyAsync(List<Access> accessList);
    }
}