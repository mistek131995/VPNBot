using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.UpdateAccessPosition
{
    public class Request : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MonthCount { get; set; }
        public int Price { get; set; }
        public string GooglePlayIdentifier { get; set; }
    }
}
