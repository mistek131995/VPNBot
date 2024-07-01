using Core.Repository;
using Infrastructure.Common;
using Infrastructure.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class LocationRepository(ContextFactory context) : ILocationRepository
    {
        public async Task<int> AddAsync(Core.Model.Location.Location location)
        {
            var newLocation = new Location()
            {
                Id = location.Id,
                Name = location.Name.Trim(),
                Tag = location.Tag.Trim(),
                VpnServers = location.VpnServers.Select(x => new VpnServer()
                {
                    Ip = x.Ip,
                    Port = x.Port,
                    Name = x.Name,
                    Description = x.Description,
                    UserName = x.UserName,
                    Password = x.Password,
                }).ToList()
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
                .ThenInclude(x => x.ConnectionStatistics)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
                return null;

            return new Core.Model.Location.Location(country.Id, country.Tag, country.Name, country.VpnServers
                .Select(s => new Core.Model.Location.VpnServer(s.Id, s.Ip, s.Name, s.Description, s.Port, s.UserName, s.Password, s.ConnectionStatistics
                        .Select(cs => new Core.Model.Location.ConnectionStatistic(cs.Id, cs.Date, cs.Count))
                        .ToList()))
                .ToList());
        }

        public async Task<List<Core.Model.Location.Location>> GetByIdsAsync(List<int> ids)
        {
            return await context.Locations
                .Include(x => x.VpnServers)
                .ThenInclude(x => x.ConnectionStatistics)
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(x => new Core.Model.Location.Location(x.Id, x.Tag, x.Name,
                    x.VpnServers
                    .Select(s => new Core.Model.Location.VpnServer(s.Id, s.Ip, s.Name, s.Description, s.Port, s.UserName, s.Password,
                        s.ConnectionStatistics
                        .Select(cs => new Core.Model.Location.ConnectionStatistic(cs.Id, cs.Date, cs.Count))
                        .ToList()))
                    .ToList()))
                .ToListAsync();
        }

        public async Task<Core.Model.Location.Location> GetByNameAsync(string name)
        {
            var location = await context.Locations.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());

            if (location == null)
                return null;

            return await GetByIdAsync(location.Id);
        }

        public async Task<Core.Model.Location.Location> GetByServerIdAsync(int serverId)
        {
            var server = await context.VpnServers.FirstOrDefaultAsync(x => x.Id == serverId);

            if (server == null)
                return null;

            return await GetByIdAsync(server.CountryId);
        }

        public async Task<Core.Model.Location.Location> GetByServerIpAsync(string serverIp)
        {
            var server = await context.VpnServers.FirstOrDefaultAsync(x => x.Ip == serverIp);

            if (server == null)
                return null;

            return await GetByIdAsync(server.CountryId);
        }

        public async Task<Core.Model.Location.Location> GetByTagAsync(string tag)
        {
            var location = await context.Locations.FirstOrDefaultAsync(x => x.Tag == tag);

            if (location == null)
                return null;

            return await GetByIdAsync(location.Id);
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
                CountryId = location.Id,
                Ip = x.Ip,
                Name = x.Name,
                Description = x.Description,
                Port = x.Port,
                UserName = x.UserName,
                Password = x.Password,
                ConnectionStatistics = x.Statistics.Select(s => new ConnectionStatistic()
                {
                    Id = s.Id,
                    Date = s.Date,
                    Count = s.Count,
                }).ToList()
            }).ToList();

            context.Locations.Update(dbLocation);
            await context.SaveChangesAsync();
        }
    }
}
