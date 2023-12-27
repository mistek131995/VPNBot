namespace Service.ControllerService.Service.GetFiles
{
    public class Result
    {
        public List<File> Files { get; set; }

        public class File
        {
            public int Id { get; set; }
            public string Tag { get; set; }
            public string Name { get; set; }
            public string Version { get; set; }
        }
    }
}
