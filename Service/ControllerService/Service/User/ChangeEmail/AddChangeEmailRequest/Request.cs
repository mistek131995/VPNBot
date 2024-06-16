using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangeEmail.AddChangeEmailRequest
{
    internal class Request : IRequest<bool>
    {
        public int UsertId { get; set; }
        public string Email { get; set; }
    }
}
