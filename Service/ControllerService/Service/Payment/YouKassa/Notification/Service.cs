using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Payment.YouKassa.Notification
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByPaymentGuidAsync(request.@object.id) ??
                throw new Exception($"Не найден пользователь по платежу {request.@object.id} ({request.IP})");

            var payment = user.Payments.FirstOrDefault(x => x.Guid == request.@object.id) ??
                throw new Exception("Платеж не найден");

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(payment.AccessPositionId) ??
                throw new Exception("Не удалось найти подписку");

            if (request.@object.status != "succeeded")
                throw new Exception($"Статус платежа не равен succeeded | {request.@object.status} | {request.@object.id}");

            user.UpdateAccessEndDate(accessPosition.MonthCount);

            payment.State = Core.Model.User.PaymentState.Completed;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
