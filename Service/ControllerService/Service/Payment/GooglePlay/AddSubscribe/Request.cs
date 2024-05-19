using Application.ControllerService.Common;
using Core.Model.User;

namespace Service.ControllerService.Service.Payment.GooglePlay. AddSubscribe
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }    
        public string SubscribeToken { get; set; }
        public SubscribeType SubscribeType { get; set; }
    }
}
