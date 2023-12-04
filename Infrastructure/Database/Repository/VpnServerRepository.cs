using Core.Model.VpnServer;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class VpnServerRepository(Context context) : IVpnServerRepository
    {
        public async Task<List<VpnServer>> GetAll()
        {
            return await context.VpnServers
                .Select(x => new VpnServer
                {
                    Id = x.Id,
                    Ip = x.Ip,
                    Name = x.Name,
                    Description = x.Description,
                    Port = x.Port,
                    UserName = x.UserName,
                    Password = x.Passsword,
                    UserCount = x.UserCount,
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<VpnServer> GetByIdAsync(int id)
        {
            return await context.VpnServers
                .Select(x => new VpnServer
                {
                    Id = x.Id,
                    Ip = x.Ip,
                    Name = x.Name,
                    Description = x.Description,
                    Port = x.Port,
                    UserName = x.UserName,
                    Password = x.Passsword,
                    UserCount = x.UserCount,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VpnServer> GetWithMinimalUserCountAsync()
        {
            var minUserCount = await context.VpnServers.AsNoTracking().MinAsync(x => x.UserCount);
            var vpnServer = await context.VpnServers.AsNoTracking().FirstOrDefaultAsync(x => x.UserCount == minUserCount);

            return await GetByIdAsync(vpnServer?.Id ?? 0);
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
                dbVpnServer.Passsword = vpnServer.Password;
                dbVpnServer.UserName = vpnServer.UserName;
            }

            await context.SaveChangesAsync();
        }
    }
}
