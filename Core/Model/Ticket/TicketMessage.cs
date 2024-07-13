namespace Core.Model.Ticket
{
    public class TicketMessage
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Message { get; private set; }
        public DateTime SendDate { get; private set; }
        public TicketMessageCondition Condition { get; private set; }

        public List<MessageFile> MessageFiles { get; private set; }

        public TicketMessage(int id, int userId, string message, DateTime sendDate, TicketMessageCondition condition, List<MessageFile> messageFiles)
        {
            Id = id;
            UserId = userId;
            Message = message;
            SendDate = sendDate;
            Condition = condition;
            MessageFiles = messageFiles;
        }

        public TicketMessage(int userId, string message, TicketMessageCondition condition, List<MessageFile> messageFiles)
        {
            UserId = userId;
            Message = message;
            SendDate = DateTime.Now;
            Condition = condition;
            MessageFiles = messageFiles;
        }

        public void AddFile(MessageFile file)
        {
            MessageFiles.Add(file);
        }
    }
}
