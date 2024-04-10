using Application.ControllerService.Common;
using Core.Common;
using File = Core.Model.File.File;

namespace Service.ControllerService.Service.UploadFile
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {


            return true;
        }
    }
}
