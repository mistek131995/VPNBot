using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.FileManager.UploadFile
{
    internal class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var byteArray = Convert.FromBase64String(request.Data.Split(",")[1]);

            var filePath = Path.Combine(request.Path, request.FileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            await File.WriteAllBytesAsync(filePath, byteArray);

            return true;
        }
    }
}
