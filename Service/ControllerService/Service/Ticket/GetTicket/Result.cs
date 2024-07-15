namespace Service.ControllerService.Service.Ticket.GetTicket
{
    public class Result
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TicketMessage> TicketMessages { get; set; }

        public class TicketMessage
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Message { get; set; }
            public DateTime Date { get; set; }

            public List<File> Files { get; set; }
        }

        public class File
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
        }
    }
}
