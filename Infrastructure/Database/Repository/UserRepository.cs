using Core.Model.User;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class UserRepository(Context context) : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(new Entity.User()
            {
                TelegramUserId = user.TelegramUserId,
                TelegramChatId = user.TelegramChatId,
                RegisterDate = user.RegisterDate,
                Role = UserRole.User,
                Login = user.Login,
                Password = user.Password
            });

            await context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users
                .Include(x => x.Access)
                .Include(x => x.Payments)
                .Select(x => new User()
                {
                    Id = x.Id,
                    TelegramUserId = x.TelegramUserId,
                    TelegramChatId = x.TelegramChatId,
                    Login = x.Login,
                    Password = x.Password,
                    RegisterDate = x.RegisterDate,
                    Role = x.Role,
                    Access = new Access()
                    {
                        Id = x.Access.Id,
                        UserId = x.Access.UserId,
                        EndDate = x.Access.EndDate,
                        AccessName = x.Access.AccessName,
                        Guid = x.Access.Guid,
                        Fingerprint = x.Access.Fingerprint,
                        Security = x.Access.Security,
                        Network = x.Access.Network,
                        PublicKey = x.Access.PublicKey,
                        ServerName = x.Access.ServerName,
                        ShortId = x.Access.ShortId,
                        Port = x.Access.Port,
                        VpnServerId = x.Access.VpnServerId,
                        IsDeprecated = x.Access.IsDeprecated
                    },
                    Payments = x.Payments.Select(p => new Payment()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        AccessPositionId = p.AccessPositionId,
                        Date = p.Date,
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetByIdsAsync(List<int> ids)
        {
            return await context.Users
                .Include(x => x.Access)
                .Include(x => x.Payments)
                .Select(x => new User()
                {
                    Id = x.Id,
                    TelegramUserId = x.TelegramUserId,
                    TelegramChatId = x.TelegramChatId,
                    Login = x.Login,
                    Password = x.Password,
                    RegisterDate = x.RegisterDate,
                    Role = x.Role,
                    Access = new Access()
                    {
                        Id = x.Access.Id,
                        UserId = x.Access.UserId,
                        EndDate = x.Access.EndDate,
                        AccessName = x.Access.AccessName,
                        Guid = x.Access.Guid,
                        Fingerprint = x.Access.Fingerprint,
                        Security = x.Access.Security,
                        Network = x.Access.Network,
                        PublicKey = x.Access.PublicKey,
                        ServerName = x.Access.ServerName,
                        ShortId = x.Access.ShortId,
                        Port = x.Access.Port,
                        VpnServerId = x.Access.VpnServerId,
                        IsDeprecated = x.Access.IsDeprecated
                    },
                    Payments = x.Payments.Select(p => new Payment()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        AccessPositionId = p.AccessPositionId,
                        Date = p.Date,
                    }).ToList()
                })
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<List<User>> GetByAccessDateRangeAsync(DateTime start, DateTime end)
        {
            var ids = await context.Users
                .Include(x => x.Access)
                .Where(x => x.Access.EndDate > start && x.Access.EndDate < end)
                .Select(x => x.Id)
                .ToListAsync();

            return await GetByIdsAsync(ids);
        }

        public async Task<User> GetByTelegramUserIdAndAccessGuidAsync(long telegramUserId, Guid accessGuid)
        {
            var user = await context.Users
                .Include(x => x.Access)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId && x.Access.Guid == accessGuid);

            return await GetByIdAsync(user.Id);
        }

        public async Task<User> GetByTelegramUserIdAsync(long telegramUserId)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            return await GetByIdAsync(user?.Id ?? 0);
        }

        public async Task UpdateAsync(User user)
        {
            var dbUser = await context.Users
                .Include(x => x.Access)
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            if (dbUser != null) 
            { 
                dbUser.Login = user.Login;
                dbUser.Password = user.Password;
                dbUser.Role = user.Role;

                user.Access = new Access()
                {
                    EndDate = user.Access.EndDate,
                    AccessName = user.Access.AccessName,
                    Guid = user.Access.Guid,
                    Fingerprint = user.Access.Fingerprint,
                    Security = user.Access.Security,
                    Network = user.Access.Network,
                    PublicKey = user.Access.PublicKey,
                    ServerName = user.Access.ServerName,
                    ShortId = user.Access.ShortId,
                    Port = user.Access.Port,
                    VpnServerId = user.Access.VpnServerId,
                    IsDeprecated = user.Access.IsDeprecated,
                };

                await context.SaveChangesAsync();
            }
        }

        public Task UpdateManyAsync(List<User> users)
        {
            throw new NotImplementedException();
        }
    }
}
