using Application.ControllerService.Common;

namespace Service.ControllerService.Service.GetConnection
{
    public class Request : IRequest<Result>
    {
        public int CountryId { get; set; }
        public int UserId { get; set; }
    }
}
