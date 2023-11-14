using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Access
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime EndDate { get; set; }
        public User User { get; set; }
    }
}
