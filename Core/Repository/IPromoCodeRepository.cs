using Core.Model.Finance;

namespace Core.Repository
{
    public interface IPromoCodeRepository
    {
        public Task<List<PromoCode>> GetAllAsync();
        public Task<PromoCode> GetByIdAsync(int id);
        public Task<List<PromoCode>> GetByIdsAsync(List<int> ids);
        public Task<PromoCode> AddAsync(PromoCode promoCode);
        public Task UpdateAsync(PromoCode promoCode);
    }
}
