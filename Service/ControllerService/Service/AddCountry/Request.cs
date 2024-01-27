using Application.ControllerService.Common;

namespace Service.ControllerService.Service.AddCountry
{
    public class Request : IRequest<bool>
    {
        public string Name { get; set; }
        public string Tag { get; set; }
    }
}
