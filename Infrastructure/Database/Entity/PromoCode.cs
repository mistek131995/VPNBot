using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class PromoCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public int UsageCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
