using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Entity
{
    public class UserConnection
    {
        [Key]
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
        public DateTime AccessEndDate { get; set; }

        public VpnServer VpnServer { get; set; }
        public User User { get; set; }
    }
}
