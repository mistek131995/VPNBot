using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Payment.CryptoCloud.Notification
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByPaymentGuidAsync(request.order_id) ??
                throw new Exception("Не найден пользователь по платежу");

            var payment = user.Payments.FirstOrDefault(x => x.Guid == request.order_id) ??
                throw new Exception("Платеж не найден");

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(payment.AccessPositionId) ??
                throw new Exception("Не удалось найти подписку");

            if (request.status != "success")
                throw new Exception("Статус платежа не равен success");

            user.UpdateAccessEndDate(accessPosition.MonthCount);

            payment.State = Core.Model.User.PaymentState.Completed;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
