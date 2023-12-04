using Database.Model;

namespace Database.Repository.Interface
{
    public interface IVpnServerRepository
    {
        public Task<VpnServer> GetWithMinUserCountAsync();
        public Task<VpnServer> GetByIp(string ip);
        public Task<VpnServer> UpdateVpnServerAsync(VpnServer vpnServer);
    }
}
