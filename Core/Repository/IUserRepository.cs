﻿using Core.Model.User;

namespace Core.Repository
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllAsync();
        public Task<List<User>> GetAllAdminsAsync();
        public Task<User> GetByIdAsync(int id);
        public Task<List<User>> GetByIdsAsync(List<int> ids);
        public Task<List<User>> GetByParentIdAsync(int parentId);
        public Task<User> GetByTelegramUserIdAsync(long telegramUserId);
        public Task<User> GetByLoginAsync(string login);
        public Task<User> GetByEmailAsync(string email);
        public Task<List<User>> GetByEmailsAsync(List<string> emails);
        public Task<User> GetByLoginAndPasswordAsync(string login, string password);
        public Task<User> GetByGuidAsync(Guid guid);
        public Task<User> GetByPaymentGuidAsync(Guid paymentId);
        public Task<User> GetBySubscribeTokenAsync(string subscribeToken);
        public Task<List<User>> GetByRegisterDateRangeAsync(DateTime start, DateTime end);
        public Task<User> GetByChangePasswordRequestGuidAsync(Guid guid);
        public Task<User> GetByChangeEmailRequestGuidAsync(Guid guid);
        public Task<User> AddAsync(User user);
        public Task<User> UpdateAsync(User user);
        public Task UpdateManyAsync(List<User> users);
        public Task DeleteAsync(User user);
    }
}
