using Core.Model.Support;

namespace Service.ControllerService.Service.GetUserTickets
{
    public class Result
    {
        public List<Ticket> Tickets { get; set; }

        public class Ticket
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public TicketCondition Condition { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}
