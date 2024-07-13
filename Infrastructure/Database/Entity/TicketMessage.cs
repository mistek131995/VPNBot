using Core.Model.Ticket;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class TicketMessage
    {
        [Key]
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public TicketMessageCondition Condition { get; set; }

        public Ticket Ticket { get; set; }
        public List<MessageFile> MessageFiles { get; set; }
    }
}
