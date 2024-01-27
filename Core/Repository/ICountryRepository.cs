using Core.Model.Country;

namespace Core.Repository
{
    public interface ICountryRepository
    {
        public Task<Country> GetById(int id);
        public Task<List<Country>> GetAllAsync();
        public Task<List<Country>> GetAllWithServersAsync();
        public Task<Country> GetByNameAsync(string name);
        public Task AddAsync(Country country);
    }
}
