﻿using Core.Model.Finance;

namespace Core.Repository
{
    public interface IAccessPositionRepository
    {
        public Task<AccessPosition> GetByIdAsync(int id);
        public Task<List<AccessPosition>> GetByIds(List<int> ids);
        public Task<AccessPosition> GetByPriceAsync(int price);
        public Task<List<AccessPosition>> GetAllAsync();
        public Task AddAsync(AccessPosition accessPosition);
        public Task UpdateAsync(AccessPosition accessPosition);
    }
}
