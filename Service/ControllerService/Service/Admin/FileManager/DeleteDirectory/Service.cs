using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.FileManager.DeleteDirectory
{
    internal class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var directoryExist = Directory.Exists(request.Path);

            if (directoryExist)
                Directory.Delete(request.Path, true);

            return directoryExist;
        }
    }
}
