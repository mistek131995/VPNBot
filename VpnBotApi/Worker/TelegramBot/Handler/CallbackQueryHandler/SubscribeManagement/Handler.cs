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

            var access = await provider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                ?? throw new UserOrAccessNotFountException("Ваш ползователь и доступ не найдены. Очистите чат с ботом и получите доступ."); ;

            if (access.EndDate <= DateTime.Now)
            {
                response.Text = $"Ваша подписка закончилась {access.EndDate.ToString("dd.MM.yyyy")} Чтобы продолжить использовать сервис, продлите подписку.";
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
                response.Text = $"Ваша подписка действительна до {access.EndDate.ToString("dd.MM.yyyy")}";
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
