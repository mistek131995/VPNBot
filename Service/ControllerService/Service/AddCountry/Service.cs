using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.AddCountry
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Tag))
                throw new HandledException("Заполните название и метку");

            var location = await repositoryProvider.LocationRepository.GetByNameAsync(request.Name);

            if (location != null)
                throw new HandledException("Страна с таким названием уже добавлена.");

            location = await repositoryProvider.LocationRepository.GetByTagAsync(request.Tag);

            if (location != null)
                throw new HandledException("Страна с таким тегом уже добавлена.");

            await repositoryProvider.LocationRepository.AddAsync(new Core.Model.Location.Location()
            {
                Name = request.Name,
                Tag = request.Tag,
            });

            return true;
        }
    }
}
