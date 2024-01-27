using Core.Model.VpnServer;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class VpnServerRepository(Context context) : IVpnServerRepository
    {
        public async Task AddAsync(VpnServer vpnServer)
        {
            await context.VpnServers.AddAsync(new Entity.VpnServer()
            {
                Ip = vpnServer.Ip,
                Port = vpnServer.Port,
                Name = vpnServer.Name,
                Description = vpnServer.Description,
                CountryId = vpnServer.CountryId,
                UserName = vpnServer.UserName,
                Password = vpnServer.Password
            });

            await context.SaveChangesAsync();
        }

        public async Task<List<VpnServer>> GetAllAsync()
        {
            return await context.VpnServers
                .Select(x => new VpnServer(x.Id, x.Ip, x.Name, x.Description, x.Port, x.UserCount, x.UserName, x.Password, x.CountryId))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<VpnServer>> GetByCountryIdAsync(int countryId)
        {
            var vpnServerIds = await context.VpnServers
                .Where(x => x.CountryId == countryId)
                .Select(x => x.Id)
                .ToListAsync();

            return await GetByIdsAsync(vpnServerIds);
        }

        public async Task<VpnServer> GetByIdAsync(int id)
        {
            var vpnServer = await context.VpnServers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if(vpnServer == null)
                return null;

            return new VpnServer(
                vpnServer.Id, 
                vpnServer.Ip, 
                vpnServer.Name, 
                vpnServer.Description, 
                vpnServer.Port, 
                vpnServer.UserCount, 
                vpnServer.UserName, 
                vpnServer.Password, 
                vpnServer.CountryId);
        }

        public async Task<List<VpnServer>> GetByIdsAsync(List<int> ids)
        {
            return await context.VpnServers
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(x => new VpnServer(x.Id, x.Ip, x.Name, x.Description, x.Port, x.UserCount, x.UserName, x.Password, x.CountryId))
                .ToListAsync();
        }

        public async Task UpdateManyAsync(List<VpnServer> vpnServers)
        {
            var vpnServerIds = vpnServers
                .Select(x => x.Id)
                .ToList();

            var dbVpnServers = await context.VpnServers
                .Where(x => vpnServerIds.Contains(x.Id))
                .ToListAsync();

            foreach(var dbVpnServer in dbVpnServers)
            {
                var vpnServer = vpnServers.FirstOrDefault(x => x.Id == dbVpnServer.Id);

                dbVpnServer.Ip = vpnServer.Ip;
                dbVpnServer.Name = vpnServer.Name;
                dbVpnServer.Description = vpnServer.Description;
                dbVpnServer.Port = vpnServer.Port;
                dbVpnServer.UserCount = vpnServer.UserCount;
                dbVpnServer.Password = vpnServer.Password;
                dbVpnServer.UserName = vpnServer.UserName;
            }

            await context.SaveChangesAsync();
        }
    }
}
