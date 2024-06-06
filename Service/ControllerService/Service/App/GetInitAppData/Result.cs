namespace Service.ControllerService.Service.App.GetInitAppData
{
    public class Result
    {
        public List<Location> Locations { get; set; }
        public string IpLocation { get; set; }
        public bool IsExpired { get; set; }
        public string AccessEndDate { get; set; }

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
