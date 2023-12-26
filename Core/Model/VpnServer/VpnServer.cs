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
        public int CountryId { get; set; }

        public VpnServer(int id, string ip, string name, string description, int port, int userCount, string userName, string password, int countryId)
        {
            Id = id;
            Ip = ip;
            Name = name;
            Description = description;
            Port = port;
            UserCount = userCount;
            UserName = userName;
            Password = password;
            CountryId = countryId;
        }
    }
}
