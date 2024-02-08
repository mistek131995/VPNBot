using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Database.Entity
{
    [Table("Countries")]
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Tag {  get; set; }
        public string Name { get; set; }

        public List<VpnServer> VpnServers { get; set; }
    }
}
