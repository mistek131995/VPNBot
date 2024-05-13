using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.FileManager.UploadFile
{
    internal class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var byteArray = Convert.FromBase64String(request.Data.Split(",")[1]);

            await File.WriteAllBytesAsync(Path.Combine(request.Path, request.FileName), byteArray);

            return true;
        }
    }
}
