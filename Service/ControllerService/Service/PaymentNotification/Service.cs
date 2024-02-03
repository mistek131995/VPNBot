﻿using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Serilog;

namespace Service.ControllerService.Service.PaymentNotification
{
    internal class Service(IRepositoryProvider repositoryProvider, ILogger logger) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var sign = MD5Hash.Hash.GetMD5($"{request.MERCHANT_ID}:{request.AMOUNT}:T52ClLdiMg){{0!L:{request.MERCHANT_ORDER_ID}");

            if (sign == request.SIGN)
            {
                var user = await repositoryProvider.UserRepository.GetByIdAsync(request.MERCHANT_ORDER_ID);
                var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.us_position_id);

                user.Payments.Add(new Payment()
                {
                    AccessPositionId = accessPosition.Id,
                    Date = DateTime.Now,
                    UserId = user.Id,
                    Amount = accessPosition.Price
                });

                if(user.AccessEndDate == null || user.AccessEndDate < DateTime.Now)
                {
                    user.AccessEndDate = DateTime.Now.AddMonths(accessPosition.MonthCount).Date;
                }
                else
                {
                    user.AccessEndDate = user.AccessEndDate?.AddMonths(accessPosition.MonthCount).Date;
                }

                await repositoryProvider.UserRepository.UpdateAsync(user);

                logger.Information($"Успешная оплата, пользователь {user.Id}, на сумуу {request.AMOUNT}.");

                return true;
            }

            return false;
        }
    }
}
