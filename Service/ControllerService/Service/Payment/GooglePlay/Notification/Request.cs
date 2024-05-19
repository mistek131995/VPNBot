using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.GooglePlay.Notification
{
    public class Request : IRequest<bool>
    {
        public Message message { get; set; }
        public string subscription { get; set; }

        public class Message
        {
            public string data { get; set; }
            public string messageId { get; set; }
            public string message_id { get; set; }
            public DateTime publishTime { get; set; }
            public DateTime publish_time { get; set; }
        }
    }
}
