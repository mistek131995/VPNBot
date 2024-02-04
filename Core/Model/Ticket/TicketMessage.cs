namespace Core.Model.Support
{
    public class TicketMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
        public TicketMessageCondition Condition { get; set; }
    }
}
