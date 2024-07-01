using Core.Model.Finance;
using Core.Repository;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class PromoCodeRepository(ContextFactory context) : IPromoCodeRepository
    {
        public async Task<List<PromoCode>> GetAllAsync()
        {
            var promoCodes = await context.PromoCodes
                .AsNoTracking()
                .ToListAsync();

            return promoCodes.Select(x => new PromoCode()
            {
                Id = x.Id,
                Code = x.Code,
                Discount = x.Discount,
                UsageCount = x.UsageCount,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).ToList();
        }

        public async Task<PromoCode> GetByIdAsync(int id)
        {
            var promoCode = await context.PromoCodes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if(promoCode == null)
                return null;

            return new PromoCode()
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                Discount = promoCode.Discount,
                UsageCount = promoCode.UsageCount,
                StartDate = promoCode.StartDate,
                EndDate = promoCode.EndDate
            };
        }

        public async Task<List<PromoCode>> GetByIdsAsync(List<int> ids)
        {
            var promoCodes = await context.PromoCodes
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            return promoCodes.Select(x => new PromoCode()
            {
                Id = x.Id,
                Code = x.Code,
                Discount = x.Discount,
                UsageCount = x.UsageCount,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            }).ToList();
        }

        public async Task<PromoCode> AddAsync(PromoCode promoCode)
        {
            var promoCodeEntity = new Entity.PromoCode()
            {
                Code = promoCode.Code,
                Discount = promoCode.Discount,
                UsageCount = promoCode.UsageCount,
                StartDate = promoCode.StartDate,
                EndDate = promoCode.EndDate
            };

            context.PromoCodes.Add(promoCodeEntity);
            await context.SaveChangesAsync();

            return new PromoCode()
            {
                Id = promoCodeEntity.Id,
                Code = promoCodeEntity.Code,
                Discount = promoCodeEntity.Discount,
                UsageCount = promoCodeEntity.UsageCount,
                StartDate = promoCodeEntity.StartDate,
                EndDate = promoCodeEntity.EndDate
            };
        }

        public async Task UpdateAsync(PromoCode promoCode)
        {
            var promoCodeEntity = await context.PromoCodes.FirstOrDefaultAsync(x => x.Id == promoCode.Id);

            promoCodeEntity.Code = promoCode.Code;
            promoCodeEntity.Discount = promoCode.Discount;
            promoCodeEntity.UsageCount = promoCode.UsageCount;
            promoCodeEntity.StartDate = promoCode.StartDate;
            promoCodeEntity.EndDate = promoCode.EndDate;

            await context.SaveChangesAsync();
        }

        public async Task<PromoCode> GetByCodeAsync(string code)
        {
            var promoCode = await context.PromoCodes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Code == code);

            if(promoCode == null)
                return null;

            return await GetByIdAsync(promoCode.Id);
        }

        public async Task DeleteAsync(PromoCode promoCode)
        {
            var promoCodeEntity = await context.PromoCodes.FirstOrDefaultAsync(x => x.Id == promoCode.Id);
            context.PromoCodes.Remove(promoCodeEntity);
            await context.SaveChangesAsync();
        }
    }
}
