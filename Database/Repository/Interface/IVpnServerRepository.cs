using Database.Model;

namespace Database.Repository.Interface
{
    public interface IVpnServerRepository
    {
        public Task<VpnServer> GetWithMinUserCountAsync();
        public Task<VpnServer> UpdateVpnServerAsync(VpnServer vpnServer);
    }
}
