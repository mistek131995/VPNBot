using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Serilog;
using Service.ControllerService.Common;
using Telegram.Bot;

namespace Service.ControllerService.Service.PaymentNotification
{
    internal class Service(IRepositoryProvider repositoryProvider, ILogger logger) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var sign = MD5Hash.Hash.GetMD5($"{request.MERCHANT_ID}:{request.AMOUNT}:-V9V(-Bb}}UXdAB}}:{request.MERCHANT_ORDER_ID}");

            if (sign == request.SIGN)
            {
                var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();
                var user = await repositoryProvider.UserRepository.GetByIdAsync(request.MERCHANT_ORDER_ID);
                var accessPosition = await repositoryProvider.AccessPositionRepository.GetByPriceAsync(request.AMOUNT);

                user.Payments.Add(new Payment()
                {
                    AccessPositionId = accessPosition.Id,
                    Date = DateTime.Now,
                    UserId = user.Id,
                });

                await repositoryProvider.UserRepository.UpdateAsync(user);

                var telegramBotClient = new TelegramBotClient(settings.TelegramToken);
                await telegramBotClient.SendTextMessageAsync(user.TelegramChatId, $"Подписка сроком до {user.Access.EndDate.ToString("dd.MM.yyyy")} успешно оплачена и активирована.");

                logger.Information($"Успешная оплата, пользователь {user.Id}, на сумуу {request.AMOUNT}.");

                return true;
            }

            //logger.Error($ {request.MERCHANT_ORDER_ID}, на сумуу {request.AMOUNT}.");

            return false;
        }
    }
}
