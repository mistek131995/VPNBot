using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Access
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }
        public string Ip {  get; set; }
        public string Type { get; set; }
        public string Security {  get; set; }
        public string Fp { get; set; }
        public string Pbk { get; set; }
        public string Sni { get; set; }
        public string Sid { get; set; }
        public string Spx { get; set; }
        public DateTime EndDate { get; set; }
        public User User { get; set; }
    }
}
