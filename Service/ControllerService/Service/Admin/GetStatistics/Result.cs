namespace Service.ControllerService.Service.Admin.GetStatistics
{
    public class Result
    {
        public int RegisterToday { get; set; }
        public int RegisterYesterday { get; set; }
        public int ConnectionCount { get; set; }

        public List<ConnectionByLocation> ConnectionByLocations { get; set; }

        public record ConnectionByLocation(string Name, int CountToday, int CountYesterday, int CountTotal);
    }
}
