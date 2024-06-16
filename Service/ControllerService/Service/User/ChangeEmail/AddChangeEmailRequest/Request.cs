using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangeEmail.AddChangeEmailRequest
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
