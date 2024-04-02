using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.Lava.Notification
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var key = "d5f2488a613154ee8c0e2e80ce81e1dc236bb3a7";

            var query = new
            {
                request.invoice_id,
                request.status,
                request.pay_time,
                request.amount,
                request.credited
            };
            var serializeQuery = Newtonsoft.Json.JsonConvert.SerializeObject(query);
            var signature = Signature.GenerateSignature(serializeQuery, "30e27bc2cba9cb964ae0e86243058cc90c4e9d62");

            if (signature != request.Signature)
                throw new Exception("Сигнатуры не совпадают");


            //var user = await repositoryProvider.UserRepository.GetByPaymentSignature(Guid.Parse(request.invoice_id))
            //    ?? throw new Exception($"Не удалось найти пользователя по сигнатуре - {request.Signature}");

            //var payment = user.Payments.FirstOrDefault(x => x.Guid == Guid.Parse(request.invoice_id))
            //    ?? throw new Exception($"Не удалось найти платеж по сигнатуре - {request.Signature}");

            //var paymentPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(payment.AccessPositionId)
            //    ?? throw new Exception($"Не удалось найти подписку с Id - {payment.AccessPositionId}");

            //if (user.AccessEndDate == null || user.AccessEndDate < DateTime.Now)
            //    user.AccessEndDate = DateTime.Now.AddMonths(paymentPosition.MonthCount);
            //else
            //    user.AccessEndDate = user.AccessEndDate?.AddMonths(paymentPosition.MonthCount);

            //payment.State = PaymentState.Completed;

            //await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
