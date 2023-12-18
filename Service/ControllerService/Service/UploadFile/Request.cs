using Application.ControllerService.Common;

namespace Service.ControllerService.Service.UploadFile
{
    public class Request : IRequest<bool>
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}
