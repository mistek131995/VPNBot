using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.FileManager.DeleteFile
{
    public class Request : IRequest<bool>
    {
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}
