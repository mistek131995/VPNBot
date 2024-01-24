using Core.Model.User;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Model = Core.Model.User;

namespace Infrastructure.Database.Repository
{
    public class UserRepository(Context context) : IUserRepository
    {
        public async Task<List<User>> GetAllWithActiveAccessAsync()
        {
            var userIds = await context.Users
                .Include(x => x.Access)
                .Where(x => !x.Access.IsDeprecated)
                .AsNoTracking()
                .Select(x => x.Id)
                .ToListAsync();

            return await GetByIdsAsync(userIds);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await context.Users
                .Include(x => x.Access)
                .Include(x => x.Payments)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return null;

            return new User()
            {
                Id = user.Id,
                TelegramUserId = user.TelegramUserId,
                TelegramChatId = user.TelegramChatId,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                RegisterDate = user.RegisterDate,
                AccessEndDate = user.AccessEndDate,
                Role = user.Role,
                Sost = user.Sost,
                Access = user.Access != null ? new Access()
                {
                    Id = user.Access.Id,
                    UserId = user.Access.UserId,
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
                    IsDeprecated = user.Access.IsDeprecated
                } : null,
                Payments = user.Payments.Select(p => new Payment()
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    AccessPositionId = p.AccessPositionId,
                    Date = p.Date,
                }).ToList()
            };
        }

        public async Task<List<User>> GetByIdsAsync(List<int> ids)
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
                    Email = x.Email,
                    Password = x.Password,
                    Sost = x.Sost,
                    RegisterDate = x.RegisterDate,
                    AccessEndDate = x.AccessEndDate,
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

        public async Task<List<User>> GetByAccessDateRangeAsync(DateTime start, DateTime end)
        {
            var ids = await context.Users
                .Include(x => x.Access)
                .Where(x => x.Access.EndDate > start && x.Access.EndDate < end)
                .AsNoTracking()
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
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            return await GetByIdAsync(user?.Id ?? 0);
        }

        public async Task<User> AddAsync(User user)
        {
            var newUser = new Entity.User()
            {
                TelegramUserId = user.TelegramUserId,
                TelegramChatId = user.TelegramChatId,
                RegisterDate = user.RegisterDate,
                Role = UserRole.User,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Sost = user.Sost
            };

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return await GetByIdAsync(newUser.Id);
        }

        public async Task UpdateAsync(User user)
        {
            var dbUser = await context.Users
                .Include(x => x.Payments)
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            dbUser.TelegramUserId = user.TelegramUserId;
            dbUser.TelegramChatId = user.TelegramChatId;
            dbUser.Login = user.Login;
            dbUser.Password = user.Password;
            dbUser.Email = user.Email;
            dbUser.Role = user.Role;
            dbUser.Sost = user.Sost;
            dbUser.RegisterDate = user.RegisterDate;
            dbUser.AccessEndDate = user.AccessEndDate;

            var newPayments = user.Payments
                .Where(x => x.Id == 0)
                .Select(x => new Entity.Payment()
                {
                    Id = x.Id,
                    AccessPositionId = x.AccessPositionId,
                    UserId = x.UserId,
                    Date = x.Date,
                })
                .ToList();

            dbUser.Payments.AddRange(newPayments);

            context.Users.Update(dbUser);
            await context.SaveChangesAsync();
        }

        public async Task UpdateManyAsync(List<User> users)
        {
            var userIds = users
                .Select(x => x.Id)
                .ToList();
            var dbUsers = await context.Users
                .Where(x => userIds.Contains(x.Id))
                .ToListAsync();

            foreach (var dbUser in dbUsers)
            {
                var user = users.FirstOrDefault(x => x.Id == dbUser.Id);

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
                    IsDeprecated = user.Access.IsDeprecated
                };

                dbUser.Payments = user.Payments.Select(p => new Entity.Payment()
                {
                    Id = p.Id,
                    AccessPositionId = p.AccessPositionId,
                    Date = p.Date,
                    UserId = p.UserId
                }).ToList();
            }

            context.Users.UpdateRange(dbUsers);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetByLoginAndPasswordAsync(string login, string password)
        {
            var userId = (await context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password))?.Id ?? 0;

            return await GetByIdAsync(userId);
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            var userId = (await context.Users.FirstOrDefaultAsync(x => x.Login == login))?.Id ?? 0;

            return await GetByIdAsync(userId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var userId = (await context.Users.FirstOrDefaultAsync(x => x.Email == email))?.Id ?? 0;

            return await GetByIdAsync(userId);
        }
    }
}
