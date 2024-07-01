namespace Core.Model.User
{
    public class UserConnection
    {
        public int Id { get; set; }
        public int VpnServerId { get; set; }
        public int Port { get; set; }
        public string Network { get; set; }
        public string Protocol { get; set; }
        public string Security { get; set; }
        public string PublicKey { get; set; }
        public string Fingerprint { get; set; }
        public string ServerName { get; set; }
        public string ShortId { get; set; }
        public DateTime AccessEndDate { get; set; }
        public ConnectionType ConnectionType { get; set; }

        public UserConnection(int id, int vpnServerId, int port, string network, string protocol, string security, string publicKey, string fingerprint, string serverName, string shortId, DateTime accessEndDate, ConnectionType connectionType)
        {
            if (id == 0)
                throw new Exception("Нельзя использовать этот конструктор для создания сущности");

            Id = id;
            VpnServerId = vpnServerId;
            Port = port;
            Network = network;
            Protocol = protocol;
            Security = security;
            PublicKey = publicKey;
            Fingerprint = fingerprint;
            ServerName = serverName;
            ShortId = shortId;
            AccessEndDate = accessEndDate;
            ConnectionType = connectionType;
        }

        public UserConnection(int vpnServerId, int port, string network, string protocol, string security, string publicKey, string fingerprint, string serverName, string shortId, DateTime accessEndDate, ConnectionType connectionType)
        {
            VpnServerId = vpnServerId;
            Port = port;
            Network = network;
            Protocol = protocol;
            Security = security;
            PublicKey = publicKey;
            Fingerprint = fingerprint;
            ServerName = serverName;
            ShortId = shortId;
            AccessEndDate = accessEndDate;
            ConnectionType = connectionType;
        }
    }
}
