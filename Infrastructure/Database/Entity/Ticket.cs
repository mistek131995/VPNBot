using Core.Model.Support;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    internal class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int TicketCategoryId { get; set; }
        public DateTime CreateDate { get; set; }
        public TicketCondition Condition { get; set; }
        public int UserId { get; set; }

        public TicketCategory TicketCategory { get; set; }
        public List<TicketMessage> TicketMessages { get; set; }
    }
}
