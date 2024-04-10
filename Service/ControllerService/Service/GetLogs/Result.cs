namespace Service.ControllerService.Service.GetLogs
{
    public class Result
    {
        public List<Log> Logs { get; set; }

        public class Log
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public string Level { get; set; }
            public string StackTrace { get; set; }
            public DateTime TimeStamp { get; set; }
        }
    }
}
