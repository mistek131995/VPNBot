using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using MD5Hash;

namespace Service.ControllerService.Service.Payment.PayOk.Notification
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {

        public async Task<bool> HandlingAsync(Request request)
        {
            var sign = $"f035f6dde555705f6773f8f1a2ea5af7|{request.desc}|{request.currency}|{request.shop}|{request.payment_id}|{request.amount}".GetMD5(EncodingType.UTF8);

            if (sign == request.sign)
            {
                var user = await repositoryProvider.UserRepository.GetByPaymentIdAsync(int.Parse(request.payment_id))
                    ?? throw new Exception($"Не удалось найти пользователя по payment_id - {request.payment_id}");

                var payment = user.Payments.FirstOrDefault(x => x.Id == int.Parse(request.payment_id));

                if (payment.State == PaymentState.Completed)
                    throw new Exception("Попытка повторного зачисления по уже оплаченному счета");

                var paymentPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(payment.AccessPositionId) ??
                    throw new Exception($"Не удалось найти подписку с Id - {payment.AccessPositionId}");

                if (user.AccessEndDate == null || user.AccessEndDate < DateTime.Now)
                    user.AccessEndDate = DateTime.Now.AddMonths(paymentPosition.MonthCount);
                else
                    user.AccessEndDate = user.AccessEndDate?.AddMonths(paymentPosition.MonthCount);

                payment.State = PaymentState.Completed;


                return true;
            }

            throw new Exception("Неверная подпись");
        }
    }
}
