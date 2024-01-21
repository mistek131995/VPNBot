using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    internal class Activation
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }
    }
}
