namespace Service.ControllerService.Service.Admin.TicketManagement.View
{
    public class Result
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TicketMessage> TicketMessages { get; set; }

        public class TicketMessage
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public int UserId { get; set; }
            public DateTime SendDate { get; set; }
        }
    }
}
