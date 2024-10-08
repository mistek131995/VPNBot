﻿namespace Service.ControllerService.Service.App.GetVpnLocation
{
    public class Result
    {
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
