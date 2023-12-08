using Core.Model.Log;

namespace Core.Repository
{
    public interface ILogRepository
    {
        public Task<List<Log>> GetAllAsync();
        public Task DeleteAllAsync();
        public Task DeleteByIdAsync(int id);
    }
}
