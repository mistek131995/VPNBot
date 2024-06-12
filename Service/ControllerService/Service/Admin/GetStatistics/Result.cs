namespace Service.ControllerService.Service.Admin.GetStatistics
{
    public class Result
    {
        public int RegisterToday { get; set; }
        public int RegisterTomorrow { get; set; }
        public int ConnectionCount { get; set; }

        public record ConnectionByLocation(string Name, int Count);
    }
}
