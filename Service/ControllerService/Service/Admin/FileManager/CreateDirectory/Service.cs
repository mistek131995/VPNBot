using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.FileManager.CreateDirectory
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var filesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "files");

            Directory.CreateDirectory(Path.Combine(filesPath, "test"));

            return true;
        }
    }
}
