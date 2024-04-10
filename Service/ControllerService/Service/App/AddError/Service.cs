using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.App.AddError
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            await repositoryProvider.LogRepository.AddAsync(new Core.Model.Log.Log()
            {
                Level = "Error",
                Message = request.Message,
                MessageTemplate = request.Message,
                Exception = request.StackTrace,
                TimeStamp = DateTime.Now
            });

            return true;
        }
    }
}
