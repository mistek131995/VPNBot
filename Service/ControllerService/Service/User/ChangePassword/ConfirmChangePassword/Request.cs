using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangePassword.ConfirmChangePassword
{
    public class Request : IRequest<bool>
    {
        public string Guid { get; set; }
    }
}
