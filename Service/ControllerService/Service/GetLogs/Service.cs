using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetLogs
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            result.Logs = (await repositoryProvider.LogRepository.GetAllAsync()).Select(x => new Result.Log()
            {
                Id = x.Id,
                Level = x.Level,
                Message = x.Message,
                TimeStamp = x.TimeStamp,
            }).ToList();

            return result;
        }
    }
}
