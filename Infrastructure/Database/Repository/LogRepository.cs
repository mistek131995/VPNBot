using Core.Model.Log;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class LogRepository(Context context) : ILogRepository
    {
        public async Task<bool> AddAsync(Log log)
        {
            await context.Logs.AddAsync(new Entity.Log()
            {
                Message = log.Message,
                MessageTemplate = log.MessageTemplate,
                Level = log.Level,
                TimeStamp = log.TimeStamp,
                Exception = log.Exception,
            });

            await context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAllAsync()
        {
            var logs = await context.Logs.ToListAsync();
            context.Logs.RemoveRange(logs);

            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var log = await context.Logs.FirstOrDefaultAsync(x => x.Id == id);

            context.Logs.Remove(log);

            await context.SaveChangesAsync();
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
                    TimeStamp = x.TimeStamp ?? DateTime.MinValue,
                })
                .ToListAsync();
        }

        public async Task<Log> GetByIdAsync(int id)
        {
            return await context.Logs
                .Select(x => new Log()
                {
                    Id = x.Id,
                    Message = x.Message,
                    Level = x.Level,
                    TimeStamp = x.TimeStamp ?? DateTime.MinValue,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
