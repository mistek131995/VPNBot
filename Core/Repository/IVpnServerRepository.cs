using Core.Model.VpnServer;

namespace Core.Repository
{
    public interface IVpnServerRepository
    {
        public Task<List<VpnServer>> GetAll();
        public Task<VpnServer> GetByIdAsync(int id);
        public Task<VpnServer> GetWithMinimalUserCountAsync();
    }
}
