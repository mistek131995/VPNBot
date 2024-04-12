using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.AuthWithGoogle
{
    public class Request : IRequest<string>
    {
        public string Token { get; set; }
    }
}
