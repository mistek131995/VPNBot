namespace Service.ControllerService.Service.App.GetServersByTag
{
    public class Result
    {
        public List<Server> Servers { get; set; }

        public class Server
        {
            public string Tag { get; set; }
            public string Ip { get; set; }
            public float Ping { get; set; }
        }
    }
}
