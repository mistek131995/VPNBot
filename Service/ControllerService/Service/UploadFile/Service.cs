using Application.ControllerService.Common;
using Core.Common;
using File = Core.Model.File.File;

namespace Service.ControllerService.Service.UploadFile
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            await repositoryProvider.FileRepository.AddAsync(new File(0, request.Tag, request.Name, request.Data, request.ContentType, ""));

            return true;
        }
    }
}
