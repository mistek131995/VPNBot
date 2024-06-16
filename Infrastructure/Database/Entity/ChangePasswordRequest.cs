using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    internal class ChangePasswordRequest
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }
        public string Password { get; set; }

        public User User { get; set; }
    }
}
