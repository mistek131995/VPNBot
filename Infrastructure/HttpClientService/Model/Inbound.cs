namespace Infrastructure.HttpClientService.Model
{
    public class Inbound
    {
        public Guid Guid { get; set; }
        public DateTime EndDate { get; set; }
        public string AccessName { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Network { get; set; }
        public string Security { get; set; }
        public RealitySetting RealitySettings { get; set; }

        public class RealitySetting
        {
            public List<string> ServerNames { get; set; }
            public List<string> ShortIds { get; set; }
            public Setting Settings { get; set; }
        }

        public class Setting
        {
            public string PublicKey { get; set; }
            public string Fingerprint { get; set; }
        }
    }
}
