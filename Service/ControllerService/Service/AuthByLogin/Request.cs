using Application.ControllerService.Common;

namespace Service.ControllerService.Service.AuthByLogin
{
    public class Request : IRequest<string>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
