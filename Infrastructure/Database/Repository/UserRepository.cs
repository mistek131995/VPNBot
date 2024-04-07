using Core.Model.User;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using Model = Core.Model.User;

namespace Infrastructure.Database.Repository
{
    public class UserRepository(Context context) : IUserRepository
    {
        public async Task<User> GetByIdAsync(int id)
        {
            var user = await context.Users
                .Include(x => x.Payments)
                .Include(x => x.UserConnections)
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
                Guid = user.Guid,
                ParentUserId = user.ParentUserId,
                Balance = user.Balance,
                UserConnections = user.UserConnections.Select(c => new UserConnection()
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    VpnServerId = c.VpnServerId,
                    Port = c.Port,
                    Network = c.Network,
                    Protocol = c.Protocol,
                    Security = c.Security,
                    PublicKey = c.PublicKey,
                    Fingerprint = c.Fingerprint,
                    ServerName = c.ServerName,
                    ShortId = c.ShortId,
                }).ToList(),
                Payments = user.Payments
                .OrderByDescending(x => x.Date)
                .Select(p => new Payment()
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    AccessPositionId = p.AccessPositionId,
                    Amount = p.Amount,
                    Date = p.Date,
                    State = p.State,
                }).ToList()
            };
        }

        public async Task<List<User>> GetByIdsAsync(List<int> ids)
        {
            return await context.Users
                .Include(x => x.Payments)
                .Include(x => x.UserConnections)
                .AsNoTracking()
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
                    Guid = x.Guid,
                    ParentUserId = x.ParentUserId,
                    Balance = x.Balance,
                    UserConnections = x.UserConnections.Select(c => new UserConnection()
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        VpnServerId = c.VpnServerId,
                        Port = c.Port,
                        Network = c.Network,
                        Protocol = c.Protocol,
                        Security = c.Security,
                        PublicKey = c.PublicKey,
                        Fingerprint = c.Fingerprint,
                        ServerName = c.ServerName,
                        ShortId = c.ShortId,
                    }).ToList(),
                    Payments = x.Payments
                    .OrderByDescending(x => x.Date)
                    .Select(p => new Payment()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        AccessPositionId = p.AccessPositionId,
                        Amount = p.Amount,
                        Date = p.Date,
                        State = p.State,
                    }).ToList()
                })
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
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
                Sost = user.Sost,
                Guid = user.Guid,
                Balance = user.Balance,
                ParentUserId = user.ParentUserId,
            };

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return await GetByIdAsync(newUser.Id);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var dbUser = await context.Users
                .Include(x => x.Payments)
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            dbUser.TelegramUserId = user.TelegramUserId;
            dbUser.TelegramChatId = user.TelegramChatId;
            dbUser.Password = user.Password;
            dbUser.Email = user.Email;
            dbUser.Role = user.Role;
            dbUser.Sost = user.Sost;
            dbUser.Balance = user.Balance;
            dbUser.AccessEndDate = user.AccessEndDate;

            dbUser.Payments = user.Payments
                .Select(x => new Entity.Payment()
                {
                    Id = x.Id,
                    AccessPositionId = x.AccessPositionId,
                    UserId = x.UserId,
                    Date = x.Date,
                    Amount = x.Amount,
                    State = x.State,
                })
                .ToList();

            dbUser.UserConnections = user.UserConnections
                .Select(x => new Entity.UserConnection()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    VpnServerId = x.VpnServerId,
                    Port = x.Port,
                    Network = x.Network,
                    Protocol = x.Protocol,
                    Security = x.Security,
                    PublicKey = x.PublicKey,
                    Fingerprint = x.Fingerprint,
                    ServerName = x.ServerName,
                    ShortId = x.ShortId,
                })
                .ToList();

            context.Users.Update(dbUser);
            await context.SaveChangesAsync();

            return await GetByIdAsync(user.Id);
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

                dbUser.TelegramUserId = user.TelegramUserId;
                dbUser.TelegramChatId = user.TelegramChatId;
                dbUser.Password = user.Password;
                dbUser.Email = user.Email;
                dbUser.Role = user.Role;
                dbUser.Sost = user.Sost;
                dbUser.Balance = user.Balance;
                dbUser.AccessEndDate = user.AccessEndDate;

                dbUser.Payments = user.Payments
                    .Select(p => new Entity.Payment()
                    {
                        Id = p.Id,
                        AccessPositionId = p.AccessPositionId,
                        Date = p.Date,
                        UserId = p.UserId,
                        Amount = p.Amount,
                        State = p.State,
                    })
                    .ToList();

                dbUser.UserConnections = user.UserConnections.ToArray()
                    .Select(c => new Entity.UserConnection()
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        VpnServerId = c.VpnServerId,
                        Port = c.Port,
                        Network = c.Network,
                        Protocol = c.Protocol,
                        Security = c.Security,
                        PublicKey = c.PublicKey,
                        Fingerprint = c.Fingerprint,
                        ServerName = c.ServerName,
                        ShortId = c.ShortId,
                    })
                    .ToList();
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

        public async Task<List<User>> GetByParentIdAsync(int parentId)
        {
            var userIds = await context.Users
                .AsNoTracking()
                .Where(x => x.ParentUserId == parentId)
                .Select(x => x.Id)
                .ToListAsync();

            return await GetByIdsAsync(userIds);
        }

        public async Task<User> GetByGuidAsync(Guid guid)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Guid == guid);

            if (user == null)
                return null;

            return await GetByIdAsync(user.Id);
        }

        public async Task<List<User>> GetByEmailsAsync(List<string> emails)
        {
            var users = await context.Users
                .Where(x => emails.Contains(x.Email))
                .ToListAsync();
            var userIds = users
                .Select(x => x.Id)
                .ToList();

            return await GetByIdsAsync(userIds);
        }

        public async Task<List<User>> GetAllAdminsAsync()
        {
            var users = await context.Users
                .Where(x => x.Role == UserRole.Admin)
                .ToListAsync();
            var userIds = users
                .Select(x => x.Id)
                .ToList();

            return await GetByIdsAsync(userIds);
        }

        public async Task<List<User>> GetAllAsync()
        {
            var userIds = await context.Users.Select(x => x.Id).ToListAsync();

            return await GetByIdsAsync(userIds);
        }

        public async Task<User> GetByPaymentId(int paymentId)
        {
            var payment = await context.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == paymentId);

            return await GetByIdAsync(payment.UserId);
        }

        public async Task DeleteAsync(User user)
        {
            var dbUser = await context.Users
                .Include(x => x.Payments)
                .Include(x => x.UserConnections)
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            context.Users.Remove(dbUser);
            await context.SaveChangesAsync();
        }
    }
}
