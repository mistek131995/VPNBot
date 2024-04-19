using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class UserUsedPromoCode
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PromoCodeId { get; set; }
        public DateTime UsedDate { get; set; }

        public User User { get; set; }
        public PromoCode PromoCode { get; set; }
    }
}
