using Core.Model.User;
using Core.Model.VpnServer;

namespace Infrastructure.HttpClientService
{
    public interface IHttpClientService
    {
        public Task<List<Guid>> DeleteInboundUserAsync(List<Guid> guids, VpnServer vpnServer);
        public Task<User> CreateInboundUserAsync(User user, List<VpnServer> vpnServers);
        public Task UpdateInboundUserAsync(User user, VpnServer vpnServer);
        public Task<List<(VpnServer vpnServer, int onlineUser)>> GetOnlineUser(List<VpnServer> vpnServers);
    }
}
