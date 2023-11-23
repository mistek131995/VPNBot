using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class VpnServer
    {
        [Key]
        public int Id { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCount { get; set; }

        //public Access Access { get; set; }
    }
}
