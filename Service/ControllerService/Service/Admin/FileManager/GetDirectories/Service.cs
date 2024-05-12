using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.FileManager.GetDirectories
{
    internal class Service : IControllerService<Request, string[]>
    {
        public async Task<string[]> HandlingAsync(Request request)
        {
            var filesPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "files");

            return Directory.GetDirectories(filesPath);
        }
    }
}
