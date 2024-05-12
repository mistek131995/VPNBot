using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.FileManager.GetDirectories
{
    internal class Service : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var filesPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "files");

            return new Result()
            {
                Directories = Directory.GetDirectories(filesPath).Select(x => new Result.Directory()
                {
                    Name = new DirectoryInfo(x).Name,
                    Path = x,
                }).ToList()
            };
        }
    }
}
