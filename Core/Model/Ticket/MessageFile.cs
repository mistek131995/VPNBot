namespace Core.Model.Ticket
{
    public class MessageFile
    {
        public int Id { get; private set; }
        public string FileName { get; private set; }
        public string Path { get; private set; }

        public MessageFile(int id, string fileName, string path)
        {
            Id = id;
            Path = path;
            FileName = fileName;
        }

        public MessageFile(string fileName, string path)
        {
            FileName = fileName;
            Path = path;
        }
    }
}
