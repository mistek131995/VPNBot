namespace Core.Model.Ticket
{
    public class MessageFile
    {
        public int Id { get; private set; }
        public string Path { get; private set; }

        public MessageFile(int id, string path)
        {
            Id = id;
            Path = path;
        }

        public MessageFile(string path)
        {
            Path = path;
        }
    }
}
