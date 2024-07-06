using Core.Model.User;
using Core.Repository;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class UserRepository(ContextFactory context) : IUserRepository
    {
        public async Task<User> GetByIdAsync(int id)
        {
            var user = await context.Users
                .Include(x => x.Payments)
                .Include(x => x.UserConnections)
                .Include(x => x.UserSetting)
                .Include(x => x.ChangeEmailRequest)
                .Include(x => x.ChangePasswordRequest)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return null;

            return new User(user.Id, user.TelegramUserId, user.TelegramChatId, user.Login, user.Email, user.Password, user.Role, user.RegisterDate,
                user.AccessEndDate, user.Sost, user.Guid, user.ParentUserId, user.Balance, user.LastConnection, user.SubscribeType, user.SubscribeToken,
                user.Payments.Select(x => new Payment(x.Id, x.AccessPositionId, x.Amount, x.Date, x.State, x.PromoCodeId, x.Guid, x.PaymentMethod)).ToList(),
                user.UserConnections.Select(x => new UserConnection(x.Id, x.VpnServerId, x.Port, x.Network, x.Protocol, x.Security, x.PublicKey, x.Fingerprint, x.ServerName, x.ShortId, x.AccessEndDate, x.ConnectionType)).ToList(),
                new UserSetting(user.UserSetting.Id, user.UserSetting.UseTelegramNotificationTicketMessage, user.UserSetting.UseTelegramNotificationAboutNews, user.UserSetting.UseTelegramNotificationLoginInError),
                user.ChangePasswordRequest == null ? null : new ChangePasswordRequest(user.ChangePasswordRequest.Id, user.ChangePasswordRequest.Guid, user.ChangePasswordRequest.Password),
                user.ChangeEmailRequest == null ? null : new ChangeEmailRequest(user.ChangeEmailRequest.Id, user.ChangeEmailRequest.Guid, user.ChangeEmailRequest.Email));
        }

        public async Task<List<User>> GetByIdsAsync(List<int> ids)
        {
            return await context.Users
                .Where(x => ids.Contains(x.Id))
                .Include(x => x.Payments)
                .Include(x => x.UserConnections)
                .Include(x => x.ChangeEmailRequest)
                .Include(x => x.ChangePasswordRequest)
                .AsNoTracking()
                .Select(u => new User(u.Id, u.TelegramUserId, u.TelegramChatId, u.Login, u.Email, u.Password, u.Role, u.RegisterDate,
                u.AccessEndDate, u.Sost, u.Guid, u.ParentUserId, u.Balance, u.LastConnection, u.SubscribeType, u.SubscribeToken,
                u.Payments.Select(x => new Payment(x.Id, x.AccessPositionId, x.Amount, x.Date, x.State, x.PromoCodeId, x.Guid, x.PaymentMethod)).ToList(),
                u.UserConnections.Select(x => new UserConnection(x.Id, x.VpnServerId, x.Port, x.Network, x.Protocol, x.Security, x.PublicKey, x.Fingerprint, x.ServerName, x.ShortId, x.AccessEndDate, x.ConnectionType)).ToList(),
                new UserSetting(u.UserSetting.Id, u.UserSetting.UseTelegramNotificationTicketMessage, u.UserSetting.UseTelegramNotificationAboutNews, u.UserSetting.UseTelegramNotificationLoginInError),
                u.ChangePasswordRequest == null ? null : new ChangePasswordRequest(u.ChangePasswordRequest.Id, u.ChangePasswordRequest.Guid, u.ChangePasswordRequest.Password),
                u.ChangeEmailRequest == null ? null : new ChangeEmailRequest(u.ChangeEmailRequest.Id, u.ChangeEmailRequest.Guid, u.ChangeEmailRequest.Email))).ToListAsync();
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
                AccessEndDate = user.AccessEndDate,
                Role = UserRole.User,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Sost = user.Sost,
                Guid = user.Guid,
                Balance = user.Balance,
                ParentUserId = user.ParentUserId,
                LastConnection = user.LastConnection,
                SubscribeToken = user.SubscribeToken,
                UserSetting = new Entity.UserSetting()
                {
                    UseTelegramNotificationTicketMessage = user.UserSetting.UseTelegramNotificationTicketMessage
                }
            };

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            var test = await context.Users.ToListAsync();

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
            dbUser.LastConnection = user.LastConnection;

            dbUser.UserSetting = new Entity.UserSetting()
            {
                Id = user.UserSetting.Id,
                UseTelegramNotificationTicketMessage = user.UserSetting.UseTelegramNotificationTicketMessage
            };

            dbUser.ChangePasswordRequest = user.ChangePasswordRequest == null ? null : new Entity.ChangePasswordRequest()
            {
                Id = user.ChangePasswordRequest.Id,
                UserId = dbUser.Id,
                Password = user.ChangePasswordRequest.Password,
                Guid = user.ChangePasswordRequest.Guid
            };

            dbUser.ChangeEmailRequest = user.ChangeEmailRequest == null ? null : new Entity.ChangeEmailRequest()
            {
                Id = user.ChangeEmailRequest.Id,
                UserId = dbUser.Id,
                Email = user.ChangeEmailRequest.Email,
                Guid = user.ChangeEmailRequest.Guid
            };

            dbUser.Payments = user.Payments
                .Select(x => new Entity.Payment()
                {
                    Id = x.Id,
                    AccessPositionId = x.AccessPositionId,
                    UserId = user.Id,
                    Date = x.Date,
                    Amount = x.Amount,
                    State = x.State,
                    PromoCodeId = x.PromoCodeId,
                    Guid = x.Guid,
                    PaymentMethod = x.PaymentMethod
                })
                .ToList();

            dbUser.UserConnections = user.UserConnections
                .Select(x => new Entity.UserConnection()
                {
                    Id = x.Id,
                    UserId = user.Id,
                    VpnServerId = x.VpnServerId,
                    Port = x.Port,
                    Network = x.Network,
                    Protocol = x.Protocol,
                    Security = x.Security,
                    PublicKey = x.PublicKey,
                    Fingerprint = x.Fingerprint,
                    ServerName = x.ServerName,
                    ShortId = x.ShortId,
                    AccessEndDate = x.AccessEndDate,
                    ConnectionType = x.ConnectionType,
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
                dbUser.LastConnection = user.LastConnection;

                dbUser.UserSetting = new Entity.UserSetting()
                {
                    Id = user.UserSetting.Id,
                    UseTelegramNotificationTicketMessage = user.UserSetting.UseTelegramNotificationTicketMessage
                };

                dbUser.ChangePasswordRequest = user.ChangePasswordRequest == null ? null : new Entity.ChangePasswordRequest()
                {
                    Id = user.ChangePasswordRequest.Id,
                    UserId = dbUser.Id,
                    Password = user.ChangePasswordRequest.Password,
                    Guid = user.ChangePasswordRequest.Guid
                };

                dbUser.ChangeEmailRequest = user.ChangeEmailRequest == null ? null : new Entity.ChangeEmailRequest()
                {
                    Id = user.ChangeEmailRequest.Id,
                    UserId = dbUser.Id,
                    Email = user.ChangeEmailRequest.Email,
                    Guid = user.ChangeEmailRequest.Guid
                };

                dbUser.Payments = user.Payments
                    .Select(p => new Entity.Payment()
                    {
                        Id = p.Id,
                        AccessPositionId = p.AccessPositionId,
                        Date = p.Date,
                        UserId = dbUser.Id,
                        Amount = p.Amount,
                        State = p.State,
                        PromoCodeId = p.PromoCodeId,
                        Guid = p.Guid,
                        PaymentMethod = p.PaymentMethod
                    })
                    .ToList();

                dbUser.UserConnections = user.UserConnections.ToArray()
                    .Select(c => new Entity.UserConnection()
                    {
                        Id = c.Id,
                        UserId = dbUser.Id,
                        VpnServerId = c.VpnServerId,
                        Port = c.Port,
                        Network = c.Network,
                        Protocol = c.Protocol,
                        Security = c.Security,
                        PublicKey = c.PublicKey,
                        Fingerprint = c.Fingerprint,
                        ServerName = c.ServerName,
                        ShortId = c.ShortId,
                        AccessEndDate = c.AccessEndDate,
                        ConnectionType = c.ConnectionType,

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

        public async Task<User> GetByPaymentGuidAsync(Guid guid)
        {
            var payment = await context.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Guid == guid);

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

        public async Task<User> GetBySubscribeTokenAsync(string subscribeToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.SubscribeToken == subscribeToken);

            return await GetByIdAsync(user?.Id ?? 0);
        }

        public async Task<List<User>> GetByRegisterDateRangeAsync(DateTime start, DateTime end)
        {
            var userIds = await context.Users
                .Where(x => x.RegisterDate.Date >= start.Date && x.RegisterDate.Date <= end.Date)
                .Select(x => x.Id)
                .ToListAsync();

            return await GetByIdsAsync(userIds);
        }

        public async Task<User> GetByChangePasswordRequestGuidAsync(Guid guid)
        {
            var request = await context.ChangePasswordRequests.FirstOrDefaultAsync(x => x.Guid == guid);

            if (request == null)
                return null;

            return await GetByIdAsync(request.UserId);
        }

        public async Task<User> GetByChangeEmailRequestGuidAsync(Guid guid)
        {
            var request = await context.ChangeEmailRequests.FirstOrDefaultAsync(x => x.Guid == guid);

            if (request == null)
                return null;

            return await GetByIdAsync(request.UserId);
        }
    }
}
