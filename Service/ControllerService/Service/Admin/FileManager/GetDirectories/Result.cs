namespace Service.ControllerService.Service.Admin.FileManager.GetDirectories
{
    public class Result
    {
        public List<Directory> Directories { get; set; }

        public class Directory
        {
            public string Name { get; set; }
            public string Path { get; set; }

            public List<string> Files { get; set; }
        }
    }
}
