using Application.ControllerService.Common;
using Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (user.AccessEndDate < DateTime.Now)
                user.AccessEndDate = DateTime.Now.AddMonths(accessPosition.MonthCount);
            else
                user.AccessEndDate = user.AccessEndDate?.AddMonths(accessPosition.MonthCount);

            payment.State = Core.Model.User.PaymentState.Completed;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
