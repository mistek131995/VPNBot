namespace Core.Model.Location
{
    public class VpnServer
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<ConnectionStatistic> Statistics { get; set; }

        public VpnServer(int id, string ip, string name, string description, int port, string userName, string password, List<ConnectionStatistic> statistics)
        {
            Id = id;
            Ip = ip;
            Name = name;
            Description = description;
            Port = port;
            UserName = userName;
            Password = password;
            Statistics = statistics;
        }
    }
}
