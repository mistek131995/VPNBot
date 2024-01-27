using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.GetAddEditServer
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var countries = await repositoryProvider.CountryRepository.GetAllAsync();

            if (request.ServerId > 0)
            {
                var server = await repositoryProvider.VpnServerRepository.GetByIdAsync(request.ServerId)
                    ?? throw new HandledExeption("Сервер не найден");

                var country = countries.FirstOrDefault(x => x.Id == server.CountryId);
            }

            result.Countries = countries
                .Select(x => new Result.Option(x.Id.ToString(), x.Name))
                .ToList();

            return result;
        }
    }
}
