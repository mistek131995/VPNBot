using Core.Model.Log;

namespace Core.Repository
{
    public interface ILogRepository
    {
        public Task<Log> GetByIdAsync(int id);
        public Task<List<Log>> GetAllAsync();
        public Task<bool> AddAsync(Log log);
        public Task DeleteAllAsync();
        public Task DeleteByIdAsync(int id);
    }
}
