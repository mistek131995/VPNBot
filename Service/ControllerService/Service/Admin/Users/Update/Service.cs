using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Users.Update
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.Id)
                ?? throw new HandledException("Пользователь не найден");

            user.AccessEndDate = request.AccessEndDate;

            if (!string.IsNullOrEmpty(request.Password))
                user.Password = request.Password;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
