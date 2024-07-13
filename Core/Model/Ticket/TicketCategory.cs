namespace Core.Model.Ticket
{
    public class TicketCategory
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public TicketCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public TicketCategory(string name)
        {
            Name = name;
        }
    }
}
