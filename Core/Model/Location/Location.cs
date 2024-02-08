namespace Core.Model.Location
{
    public class Location
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }

        public List<VpnServer> VpnServers { get; set; }
    }
}
