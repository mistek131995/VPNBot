namespace Service.ControllerService.Service.App.GetVpnLocation
{
    public class Result
    {
        //TODO: Удалить Countries спустя пару обновлений 07.02.24
        public List<Location> Countries { get; set; }
        public List<Location> Locations { get; set; }

        public class Location
        {
            public int Id { get; set; }
            public string Tag { get; set; }
            public string Name { get; set; }

            public List<Server> Servers { get; set; }
        }

        public class Server
        {
            public string Ip { get; set; }
            public int Ping { get; set; }
        }
    }
}
