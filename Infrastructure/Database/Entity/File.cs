namespace Infrastructure.Database.Entity
{
    public class File
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public string Version { get; set; }
    }
}
