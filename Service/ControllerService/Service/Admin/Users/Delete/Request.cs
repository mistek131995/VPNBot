using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Users.Delete
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
