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
    }
}
