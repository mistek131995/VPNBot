namespace Service.ControllerService.Service.Admin.VpnLocation.GetIndex
{
    public class Result
    {
        public List<Location> Locations { get; set; }

        public class Location
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public List<Server> Servers { get; set; }
        }

        public class Server
        {
            public int Id { get; set; }
            public string Ip { get; set; }
            public int Port { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
