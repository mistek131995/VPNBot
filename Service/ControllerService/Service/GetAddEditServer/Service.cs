using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetAddEditServer
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var countries = await repositoryProvider.LocationRepository.GetAllAsync();

            result.Countries = countries
                .Select(x => new Result.Option(x.Id.ToString(), x.Name))
                .ToList();

            return result;
        }
    }
}
