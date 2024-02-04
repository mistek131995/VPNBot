using Application.ControllerService.Common;

namespace Service.ControllerService.Service.GetSubscribeItem
{
    public class Request : IRequest<Result>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Sale {  get; set; }
    }
}
