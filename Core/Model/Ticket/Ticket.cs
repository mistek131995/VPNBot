using Service.ControllerService.Common;

namespace Core.Model.Ticket
{
    public class Ticket
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public DateTime CreateDate { get; private set; }
        public TicketCondition Condition { get; set; }
        public int UserId { get; private set; }
        private List<TicketMessage> _ticketMessages { get; set; }

        public List<TicketMessage> TicketMessages => _ticketMessages;

        public Ticket(int id, string title, int categoryId, string categoryName, DateTime createDate, TicketCondition condition, int userId, List<TicketMessage> ticketMessages)
        {
            Id = id;
            Title = title;
            CategoryId = categoryId;
            CategoryName = categoryName;
            CreateDate = createDate;
            Condition = condition;
            UserId = userId;
            _ticketMessages = ticketMessages;
        }

        public Ticket(string title, int categoryId, DateTime createDate, TicketCondition condition, int userId)
        {
            Title = title;
            CategoryId = categoryId;
            CreateDate = createDate;
            Condition = condition;
            UserId = userId;
            _ticketMessages = new List<TicketMessage>();
        }

        public void AddMessage(TicketMessage message)
        {
            if (string.IsNullOrEmpty(message.Message))
                throw new HandledException("Введите сообщение");

            _ticketMessages.Add(message);
        }
    }
}
