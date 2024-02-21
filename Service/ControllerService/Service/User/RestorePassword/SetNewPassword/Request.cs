using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.RestorePassword.SetNewPassword
{
    public class Request : IRequest<bool>
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
    }
}
