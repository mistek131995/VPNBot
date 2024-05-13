using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.FileManager.DeleteFile
{
    internal class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var filePath = Path.Combine(request.Path, request.FileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            return true;
        }
    }
}
