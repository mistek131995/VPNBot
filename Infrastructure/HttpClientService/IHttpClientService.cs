﻿using Core.Model.User;
using Core.Model.VpnServer;

namespace Infrastructure.HttpClientService
{
    public interface IHttpClientService
    {
        public Task<List<Guid>> DeleteInboundUserAsync(List<Guid> guids, VpnServer vpnServer);
        public Task<User> CreateInboundUserAsync(User user, VpnServer vpnServer);
        public Task UpdateInboundUserAsync(User user, VpnServer vpnServer);
    }
}
