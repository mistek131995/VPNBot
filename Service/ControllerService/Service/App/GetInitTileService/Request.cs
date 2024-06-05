using Application.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetInitTileService
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
