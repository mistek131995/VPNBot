using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class ResetPassword
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }

        public User User { get; set; }
    }
}
