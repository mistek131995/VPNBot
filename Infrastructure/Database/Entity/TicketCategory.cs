using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class TicketCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
