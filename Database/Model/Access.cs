using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Access
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }
        public string Ip { get; set; }
        //public int VpnServerId { get; set; }
        public int Port { get; set; }
        public string Network { get; set; }
        public string Security { get; set; }
        public string Fingerprint { get; set; }
        public string PublicKey { get; set; }
        public string ServerName { get; set; }
        public string ShortId { get; set; }
        public string AccessName { get; set; }
        public DateTime EndDate { get; set; }
        public User User { get; set; }


        //public VpnServer VpnServer { get; set; }
    }
}
