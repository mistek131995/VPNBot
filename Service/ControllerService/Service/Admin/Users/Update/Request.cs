using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Users.Update
{
    public class Request : IRequest<bool>
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public DateTime AccessEndDate { get; set; }
    }
}
