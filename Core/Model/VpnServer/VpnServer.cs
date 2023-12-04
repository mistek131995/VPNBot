using Core.Common;

namespace Core.Model.VpnServer
{
    public class VpnServer : IAggregate
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Port { get; set; }
        public int UserCount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
