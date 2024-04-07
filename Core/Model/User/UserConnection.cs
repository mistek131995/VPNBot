namespace Core.Model.User
{
    public class UserConnection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VpnServerId { get; set; }
        public int Port { get; set; }
        public string Network { get; set; }
        public string Protocol { get; set; }
        public string Security { get; set; }
        public string PublicKey { get; set; }
        public string Fingerprint { get; set; }
        public string ServerName { get; set; }
        public string ShortId { get; set; }
    }
}
