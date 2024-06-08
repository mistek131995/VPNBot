using Application.ControllerService.Common;

namespace Service.ControllerService.Service.ActivateUser
{
    public class Request : IRequest<bool>
    {
        public Guid Guid { get; set; }
    }
}
