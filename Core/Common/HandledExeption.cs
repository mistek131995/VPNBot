namespace Service.ControllerService.Common
{
    public class HandledExeption(string message, bool writeToLog = false) : Exception(message)
    {
        public bool WriteToLog { get; set; } = writeToLog;
    }
}
