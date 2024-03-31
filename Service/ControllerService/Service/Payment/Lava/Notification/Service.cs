using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;

namespace Service.ControllerService.Service.Payment.Lava.Notification
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            //var signature = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(request.Signature);

            var user = await repositoryProvider.UserRepository.GetByPaymentSignature(request.Signature)
                ?? throw new Exception($"Не удалось найти пользователя по сигнатуре - {request.Signature}");

            var payment = user.Payments.FirstOrDefault(x => x.Signature == request.Signature)
                ?? throw new Exception($"Не удалоь найти платеж по сигнатуре - {request.Signature}");

            var paymentPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(payment.AccessPositionId)
                ?? throw new Exception($"Не удалось найти подписку с Id - {payment.AccessPositionId}");

            if (user.AccessEndDate == null || user.AccessEndDate < DateTime.Now)
                user.AccessEndDate = DateTime.Now.AddMonths(paymentPosition.MonthCount);
            else
                user.AccessEndDate = user.AccessEndDate?.AddMonths(paymentPosition.MonthCount);

            payment.State = PaymentState.Completed;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
