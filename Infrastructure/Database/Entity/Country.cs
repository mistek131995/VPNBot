using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Tag {  get; set; }
        public string Name { get; set; }

        public List<VpnServer> VpnServers { get; set; }
    }
}
