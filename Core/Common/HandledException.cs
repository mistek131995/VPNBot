namespace Service.ControllerService.Common
{
    public class HandledException(string message, bool writeToLog = false) : Exception(message)
    {
        public bool WriteToLog { get; set; } = writeToLog;
    }
}
