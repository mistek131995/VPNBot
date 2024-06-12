using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Entity
{
    public class ConnectionStatistic
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }

        public VpnServer VpnServer { get; set; }
    }
}
