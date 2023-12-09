using Core.Model.Log;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class LogRepository(Context context) : ILogRepository
    {
        public async Task DeleteAllAsync()
        {
            var logs = await context.Logs.ToListAsync();
            context.Logs.RemoveRange(logs);

            context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var log = await context.Logs.FirstOrDefaultAsync(log => log.Id == id);
            context.Logs.Remove(log);

            context.SaveChangesAsync();
        }

        public async Task<List<Log>> GetAllAsync()
        {
            return await context.Logs
                .AsNoTracking()
                .Select(x => new Log()
                {
                    Id = x.Id,
                    Message = x.Message,
                    Level = x.Level,
                    TimeStamp = x.TimeStamp,
                })
                .ToListAsync();
        }

        public async Task<Log> GetByIdAsync(int id)
        {
            return await context.Logs.Select(x => new Log()
            {
                Id = x.Id,
                Message = x.Message,
                Level = x.Level,
                TimeStamp = x.TimeStamp,
            }).FirstOrDefaultAsync();
        }
    }
}
