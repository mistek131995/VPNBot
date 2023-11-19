using Database.Common;
using Database.Model;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.SubscribeManagement
{
    public class Handler(IRepositoryProvider provider) : IHandler<Query, Response>
    {
        public IRepositoryProvider provider = provider;

        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await provider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId);

            if (access == null)
            {
                response.Text = "Ваш ползователь и доступ не найдены. Очистите чат с ботом и получите доступ.";
            }
            else if (access.EndDate <= DateTime.Now)
            {
                response.Text = $"Ваша подписка закончилась {access.EndDate.ToShortTimeString()} Чтобы продолжить использовать сервис, продлите подписку.";
                response.InlineKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Продлить на 7 дней (Бесплатно).", "extendForWeek"),
                        //InlineKeyboardButton.WithCallbackData("1 месяц за 100руб.", "payForMonth")
                    }
                });
            }
            else
            {
                response.Text = $"Ваша подписка действительна до {access.EndDate.ToShortTimeString()}";
                response.InlineKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Получить QR код.", "getQrCode")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Продлить на 7 дней (Бесплатно).", "extendForWeek"),
                        //InlineKeyboardButton.WithCallbackData("1 месяц за 100руб.", "payForMonth")
                    }
                });
            }

            return response;
        }
    }
}
