using Application.ControllerService.Common;
using Application.TelegramBotService.Common;
using Core.Common;
using Core.Model.User;
using Telegram.Bot;

namespace Service.ControllerService.Service.PaymentNotification
{
    internal class Service(IRepositoryProvider repositoryProvider, ITelegramBotClient telegramBotClient) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var sign = Helper.GetMD5Hash($"{request.MERCHANT_ID}:{request.AMOUNT}:-V9V(-Bb}}UXdAB}}:{request.MERCHANT_ORDER_ID}");

            if (sign == request.SIGN)
            {
                var user = await repositoryProvider.UserRepository.GetByIdAsync(request.MERCHANT_ORDER_ID);
                var accessPosition = await repositoryProvider.AccessPositionRepository.GetByPriceAsync(request.AMOUNT);

                user.Payments.Add(new Payment()
                {
                    AccessPositionId = accessPosition.Id,
                    Date = DateTime.Now,
                    UserId = user.Id,
                });

                await repositoryProvider.UserRepository.UpdateAsync(user);

                await telegramBotClient.SendTextMessageAsync(user.TelegramChatId, $"Подписка сроком до {user.Access.EndDate.ToString("dd.MM.yyyy")} успешно оплачена и активирована.");

                Console.WriteLine("Успешно");

                return true;
            }

            Console.WriteLine("Ошибка");

            return false;
        }
    }
}
