namespace Core.Model.Support
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
        public TicketCondition Condition { get; set; }
        public int UserId { get; set; }

        public List<TicketMessage> TicketMessages { get; set; }
    }
}
