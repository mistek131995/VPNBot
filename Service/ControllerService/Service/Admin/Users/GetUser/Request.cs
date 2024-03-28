using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Users.GetUser
{
    public class Request : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
