using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class MessageFile
    {
        [Key]
        public int Id { get; set; }
        public int TicketMessageId { get; set; }
        [MaxLength(255)]
        public string Path { get; set; }

        public TicketMessage TicketMessage { get; set; }
    }
}
