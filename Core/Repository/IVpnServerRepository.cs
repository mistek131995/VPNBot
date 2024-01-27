using Core.Model.VpnServer;

namespace Core.Repository
{
    public interface IVpnServerRepository
    {
        public Task<List<VpnServer>> GetAllAsync();
        public Task<VpnServer> GetByIdAsync(int id);
        public Task<List<VpnServer>> GetByIdsAsync(List<int> ids);
        public Task<List<VpnServer>> GetByCountryIdAsync(int countryId);
        public Task UpdateManyAsync(List<VpnServer> vpnServers);
        public Task AddAsync(VpnServer vpnServer);
    }
}
