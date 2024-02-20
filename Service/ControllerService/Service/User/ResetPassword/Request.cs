using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.ResetPassword
{
    public class Request : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
