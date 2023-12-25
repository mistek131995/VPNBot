using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetFile
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var file = await repositoryProvider.FileRepository.GetByTagAsync(request.Tag)
                ?? throw new Exception("Файл с такой меткой не найден.");

            result.FileName = file.Name;
            result.Version = file.Version;
            result.DownloadLink = $"https://lockvpn.me/files/{file.Tag}/{file.Name}";

            return result;
        }
    }
}
