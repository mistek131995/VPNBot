using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Log;

namespace Service.ControllerService.Service.AddLog
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            await repositoryProvider.LogRepository.AddAsync(new Log()
            {
                Message = request.Title,
                MessageTemplate = request.StackTrace,
                Level = "Error",
                TimeStamp = DateTime.Now,
            });

            return true;
        }
    }
}
