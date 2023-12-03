using Model = Core.Model.User;
using Core.Repository;
using Entity = Infrastructure.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class UserRepository(Context context) : IUserRepository
    {
        public async Task AddAsync(Model.User user)
        {
            await context.Users.AddAsync(new Entity.User()
            {
                TelegramUserId = user.TelegramUserId,
                TelegramChatId = user.TelegramChatId,
                RegisterDate = user.RegisterDate,
                Role = Model.UserRole.User,
                Login = user.Login,
                Password = user.Password
            });

            await context.SaveChangesAsync();
        }

        public async Task<Model.User> GetByIdAsync(int id)
        {
            return await context.Users
                .Include(x => x.Access)
                .Include(x => x.Payments)
                .Select(x => new Model.User()
                {
                    Id = x.Id,
                    TelegramUserId = x.TelegramUserId,
                    TelegramChatId = x.TelegramChatId,
                    Login = x.Login,
                    Password = x.Password,
                    RegisterDate = x.RegisterDate,
                    Role = x.Role,
                    Access = new Model.Access()
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
                    Payments = x.Payments.Select(p => new Model.Payment()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        AccessPositionId = p.AccessPositionId,
                        Date = p.Date,
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Model.User>> GetByIdsAsync(List<int> ids)
        {
            return await context.Users
                .Include(x => x.Access)
                .Include(x => x.Payments)
                .Select(x => new Model.User()
                {
                    Id = x.Id,
                    TelegramUserId = x.TelegramUserId,
                    TelegramChatId = x.TelegramChatId,
                    Login = x.Login,
                    Password = x.Password,
                    RegisterDate = x.RegisterDate,
                    Role = x.Role,
                    Access = new Model.Access()
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
                    Payments = x.Payments.Select(p => new Model.Payment()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        AccessPositionId = p.AccessPositionId,
                        Date = p.Date,
                    }).ToList()
                })
                .Where(x => ids.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Model.User>> GetByAccessDateRangeAsync(DateTime start, DateTime end)
        {
            var ids = await context.Users
                .Include(x => x.Access)
                .AsNoTracking()
                .Where(x => x.Access.EndDate > start && x.Access.EndDate < end)
                .Select(x => x.Id)
                .ToListAsync();

            return await GetByIdsAsync(ids);
        }

        public async Task<Model.User> GetByTelegramUserIdAndAccessGuidAsync(long telegramUserId, Guid accessGuid)
        {
            var user = await context.Users
                .Include(x => x.Access)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId && x.Access.Guid == accessGuid);

            return await GetByIdAsync(user.Id);
        }

        public async Task<Model.User> GetByTelegramUserIdAsync(long telegramUserId)
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            return await GetByIdAsync(user?.Id ?? 0);
        }

        public async Task UpdateAsync(Model.User user)
        {
            var dbUser = new Entity.User()
            {
                Id = user.Id,
                TelegramUserId = user.TelegramUserId,
                TelegramChatId = user.TelegramChatId,
                Login = user.Login,
                Password = user.Password,
                Role = user.Role,
                RegisterDate = user.RegisterDate,
            };

            dbUser.Access = new Entity.Access()
            {
                Id = user.Access.Id,
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

            dbUser.Payments = user.Payments.Select(x => new Entity.Payment()
            {
                Id = x.Id,
                AccessPositionId = x.AccessPositionId,
                UserId = x.UserId,
                Date = x.Date,
            }).ToList();

            context.Users.Update(dbUser);
            await context.SaveChangesAsync();
        }

        public async Task UpdateManyAsync(List<Model.User> users)
        {
            var dbUsers = users.Select(x => new Entity.User()
            {
                Id = x.Id,
                TelegramUserId = x.TelegramUserId,
                TelegramChatId = x.TelegramChatId,
                Login = x.Login,
                Password = x.Password,
                Role = x.Role,
                RegisterDate = x.RegisterDate,
                Access = new Entity.Access()
                {
                    Id = x.Access.Id,
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
                Payments = x.Payments.Select(p => new Entity.Payment()
                {
                    Id = p.Id,
                    AccessPositionId = p.AccessPositionId,
                    Date = p.Date,
                    UserId = p.UserId
                }).ToList()
            }).ToList();

            context.Users.UpdateRange(dbUsers);
            await context.SaveChangesAsync();
        }
    }
}
