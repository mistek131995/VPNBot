using Core.Model.Country;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class CountryRepository(Context context) : ICountryRepository
    {
        public async Task AddAsync(Country country)
        {
            await context.Countries.AddAsync(new Entity.Country() { 
                Id = country.Id, 
                Name = country.Name.Trim(), 
                Tag = country.Tag.Trim()
            });

            await context.SaveChangesAsync();
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await context.Countries
                .AsNoTracking()
                .Include(x => x.VpnServers)
                .Where(x => x.VpnServers.Any())
                .Select(x => new Country()
                {
                    Id = x.Id,
                    Tag = x.Tag,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<Country> GetById(int id)
        {
            var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if(country == null) 
                return null;

            return new Country()
            {
                Id = country.Id,
                Name = country.Name,
                Tag = country.Tag
            };
        }

        public async Task<Country> GetByNameAsync(string name)
        {
            var country = await context.Countries.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());

            if(country == null)
                return null;

            return await GetById(country.Id);
        }
    }
}
