namespace Service.ControllerService.Service.GetServers
{
    public class Result
    {
        public List<Server> Servers { get; set; }

        public class Server
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Ip { get; set; }
            public int Port { get; set; }
            public int UserCount { get; set; }
        }
    }
}
