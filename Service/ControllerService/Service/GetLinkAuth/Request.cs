using Application.ControllerService.Common;

namespace Service.ControllerService.Service.GetLinkAuth
{
    public class Request : IRequest<Result>
    {
        public long TelegramUserId { get; set; }
        public Guid Guid { get; set; }
    }
}
