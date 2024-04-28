﻿using Application.ControllerService.Common;
using Core.Common;
using Newtonsoft.Json;

namespace Service.ControllerService.Service.Payment.YouKassa.Notification
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByPaymentGuidAsync(request.@object.id) ??
                throw new Exception("Не найден пользователь по платежу");

            var payment = user.Payments.FirstOrDefault(x => x.Guid == request.@object.id) ??
                throw new Exception("Платеж не найден");

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(payment.AccessPositionId) ??
                throw new Exception("Не удалось найти подписку");

            if (user.AccessEndDate < DateTime.Now)
                user.AccessEndDate = DateTime.Now.AddMonths(accessPosition.MonthCount);
            else
                user.AccessEndDate = user.AccessEndDate?.AddMonths(accessPosition.MonthCount);

            payment.State = Core.Model.User.PaymentState.Completed;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            Console.WriteLine("------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(request));
            Console.WriteLine("------------------------");

            return true;
        }
    }
}
