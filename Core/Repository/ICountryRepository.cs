using Core.Model.Country;

namespace Core.Repository
{
    public interface ICountryRepository
    {
        public Task<List<Country>> GetAllAsync();
    }
}
