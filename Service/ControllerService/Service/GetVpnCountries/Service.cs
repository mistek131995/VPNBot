using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetVpnCountries
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var countries = await repositoryProvider.CountryRepository.GetAllWithServersAsync();

            result.Countries = countries.Select(x => new Result.Country()
            {
                Id = x.Id,
                Tag = x.Tag,
                Name = x.Name,
            }).ToList();

            return result;
        }
    }
}
