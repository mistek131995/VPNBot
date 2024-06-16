using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangePassword.AddChangePasswordRequest
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
