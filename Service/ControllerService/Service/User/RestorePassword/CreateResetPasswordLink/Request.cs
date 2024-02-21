using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.RestorePassword.CreateResetPasswordLink
{
    public class Request : IRequest<bool>
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
