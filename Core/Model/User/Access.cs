using Core.Common;

namespace Core.Model.User
{
    public class Access : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime EndDate { get; set; }
        public string AccessName { get; set; }
        public Guid Guid { get; set; }
        public string Fingerprint { get; set; }
        public string Security { get; set; }
        public string Network { get; set; }
        public string PublicKey { get; set; }
        public string ServerName { get; set; }
        public string ShortId { get; set; }
        public int Port { get; set; }
        public int VpnServerId { get; set; }
        public bool IsDeprecated { get; set; }
    }
}
