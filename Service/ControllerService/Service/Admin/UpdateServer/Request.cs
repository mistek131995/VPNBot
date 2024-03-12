using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.UpdateServer
{
    public class Request : IRequest<bool>
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
