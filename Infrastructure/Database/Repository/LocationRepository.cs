using Core.Repository;
using Infrastructure.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class LocationRepository(Context context) : ILocationRepository
    {
        public async Task<int> AddAsync(Core.Model.Location.Location location)
        {
            var newLocation = new Location()
            {
                Id = location.Id,
                Name = location.Name.Trim(),
                Tag = location.Tag.Trim(),
                VpnServers = new List<VpnServer>()
            };

            await context.Locations.AddAsync(newLocation);
            await context.SaveChangesAsync();

            return location.Id;
        }

        public async Task<List<Core.Model.Location.Location>> GetAllAsync()
        {
            var ids = await context.Locations.AsNoTracking().Select(x => x.Id).ToListAsync();

            return await GetByIdsAsync(ids);
        }

        public async Task<Core.Model.Location.Location> GetByIdAsync(int id)
        {
            var country = await context.Locations
                .Include(x => x.VpnServers)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if(country == null) 
                return null;

            return new Core.Model.Location.Location()
            {
                Id = country.Id,
                Name = country.Name,
                Tag = country.Tag,
                VpnServers = country.VpnServers.Select(v => new Core.Model.Location.VpnServer(
                    v.Id,
                    v.Ip, 
                    v.Name, 
                    v.Description, 
                    v.Port, 
                    v.UserName, 
                    v.Password)
                ).ToList()
            };
        }

        public async Task<List<Core.Model.Location.Location>> GetByIdsAsync(List<int> ids)
        {
            var locations = await context.Locations
                .Include(x => x.VpnServers)
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            return locations.Select(x => new Core.Model.Location.Location()
            {
                Id = x.Id,
                Name = x.Name,
                Tag = x.Tag,
                VpnServers = x.VpnServers.Select(v => new Core.Model.Location.VpnServer(
                    v.Id, 
                    v.Ip, 
                    v.Name, 
                    v.Description, 
                    v.Port, 
                    v.UserName,
                    v.Password)
                ).ToList()
            }).ToList();
        }

        public async Task<Core.Model.Location.Location> GetByNameAsync(string name)
        {
            var country = await context.Locations.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());

            if(country == null)
                return null;

            return await GetByIdAsync(country.Id);
        }

        public async Task UpdateAsync(Core.Model.Location.Location location)
        {
            var dbLocation = await context.Locations
                .Include(x => x.VpnServers)
                .FirstOrDefaultAsync(x => x.Id == location.Id);

            dbLocation.Tag = location.Tag;
            dbLocation.Name = location.Name;
            dbLocation.VpnServers = location.VpnServers.Select(x => new VpnServer()
            {
                Id = x.Id,
                Ip = x.Ip,
                Name = x.Name,
                Description = x.Description,
                Port = x.Port,
                UserName = x.UserName,
                Password = x.Password,
            }).ToList();

            context.Locations.Update(dbLocation);
            await context.SaveChangesAsync();
        }
    }
}
