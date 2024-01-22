using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.ActivateUser
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var activation = await repositoryProvider.ActiovationRepository.GetByGuidAsync(request.Guid);

            if (activation == null)
                return false;

            var user = await repositoryProvider.UserRepository.GetByIdAsync(activation.UserId);
            user.Sost = UserSost.Active;
            user.AccessEndDate = DateTime.Now.AddDays(7);
            await repositoryProvider.UserRepository.UpdateAsync(user);

            await repositoryProvider.ActiovationRepository.DeleteByGuid(request.Guid);

            return true;
        }
    }
}
