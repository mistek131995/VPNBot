using Service.ControllerService.Common;

namespace Core.Model.Location
{
    public class Location
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }

        public List<VpnServer> VpnServers { get; set; }

        public void DeleteServer(int serverId)
        {
            var server = VpnServers.FirstOrDefault(x => x.Id == serverId);
            if (server == null)
            {
                throw new HandledExeption("Сервер не найден");
            }

            VpnServers.Remove(server);
        }
    }
}
