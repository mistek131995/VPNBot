namespace Infrastructure.HttpClientService.Model
{
    public class Inbound
    {
        public string Remark { get; set; }
        public int Port { get; set; }
        public _RealitySettings RealitySettings { get; set; }

        public class _RealitySettings
        {
            public bool Show { get; set; }
            public int Xver { get; set; }
            public string Dest { get; set; }
        }
    }
}
