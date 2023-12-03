using Application.ControllerService.Common;

namespace Service.ControllerService.Service.ChangePassword
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
}
