using Database.Model;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implementation
{
    public class VpnServerRepository(Context context) : IVpnServerRepository
    {
        public async Task<VpnServer> GetWithMinUserCountAsync()
        {
            var vpnServers = await context.VpnServers.ToListAsync();

            return vpnServers.FirstOrDefault(x => x.UserCount == vpnServers.Min(c => c.UserCount));
        }

        public async Task<VpnServer> UpdateVpnServerAsync(VpnServer vpnServer)
        {
            var curVpnServer = await context.VpnServers.FirstOrDefaultAsync(x => x.Id == vpnServer.Id);

            if (vpnServer.Id == 0)
            {
                curVpnServer = vpnServer;
                await context.AddAsync(curVpnServer);
            }
            else
            {
                curVpnServer.Ip = vpnServer.Ip;
                curVpnServer.Port = vpnServer.Port;
                curVpnServer.Name = vpnServer.Name;
                curVpnServer.Description = vpnServer.Description;
                curVpnServer.UserCount = vpnServer.UserCount;
            }

            await context.SaveChangesAsync();

            return curVpnServer;
        }
    }
}
