using Application.ControllerService.Common;

namespace Service.ControllerService.Service.AddServer
{
    public class Request : IRequest<bool>
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
