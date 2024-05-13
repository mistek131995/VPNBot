using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.FileManager.UploadFile
{
    public class Request : IRequest<bool>
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Data { get; set; }
    }
}
