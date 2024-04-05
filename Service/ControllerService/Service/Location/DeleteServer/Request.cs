using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Location.DeleteServer
{
    public class Request : IRequest<bool>
    {
        /// <summary>
        /// Id сервера
        /// </summary>
        public int Id { get; set; }
    }
}
