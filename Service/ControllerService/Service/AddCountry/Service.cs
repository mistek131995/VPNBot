using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Country;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.AddCountry
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Tag))
                throw new HandledExeption("Заполните название и метку");

            var country = await repositoryProvider.CountryRepository.GetByNameAsync(request.Name);

            if (country != null)
                throw new HandledExeption("Страна с таким названием уже добавлена.");

            await repositoryProvider.CountryRepository.AddAsync(new Country()
            {
                Name = request.Name,
                Tag = request.Tag,
            });

            return true;
        }
    }
}
