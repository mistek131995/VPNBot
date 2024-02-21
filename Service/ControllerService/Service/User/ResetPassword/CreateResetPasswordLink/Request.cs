using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.ResetPassword.CreateResetPasswordLink
{
    public class Request : IRequest<bool>
    {
        public string Email { get; set; }
    }
}
