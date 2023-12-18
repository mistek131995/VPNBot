using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.UploadFile
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public Task<bool> HandlingAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
