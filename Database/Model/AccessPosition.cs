using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class AccessPosition
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        public int MonthCount { get; set; }
        public int IpCount { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public Payment Payment { get; set; }
    }
}
