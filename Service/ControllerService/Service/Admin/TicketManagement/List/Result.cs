using Core.Model.Ticket;

namespace Service.ControllerService.Service.Admin.TicketManagement.List
{
    public class Result
    {
        public List<Ticket> Tickets { get; set; }

        public class Ticket
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Category { get; set; }
            public string UserName { get; set; }
            public TicketCondition Condition { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}
