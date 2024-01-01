using Core.Common;

namespace Core.Model.Log
{
    public class Log : IAggregate
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
