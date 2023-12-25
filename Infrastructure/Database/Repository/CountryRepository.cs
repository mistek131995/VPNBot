using Core.Model.Country;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class CountryRepository(Context context) : ICountryRepository
    {
        public async Task<List<Country>> GetAllAsync()
        {
            return await context.Countries
                .Include(x => x.VpnServers)
                .Where(x => x.VpnServers.Any())
                .Select(x => new Country()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}
