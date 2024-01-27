namespace Service.ControllerService.Service.GetAddEditServer
{
    public class Result
    {
        public List<Option> Countries { get; set; }

        public record Option(string Value, string Label);
    }
}
