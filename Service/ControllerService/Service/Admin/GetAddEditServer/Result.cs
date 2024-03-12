namespace Service.ControllerService.Service.Admin.GetAddEditServer
{
    public class Result
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
        public List<Option> Countries { get; set; }

        public record Option(string Value, string Label);
    }
}
