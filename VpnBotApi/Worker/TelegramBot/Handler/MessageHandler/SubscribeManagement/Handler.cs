using Database.Common;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.SubscribeManagement
{
    public class Handler(IRepositoryProvider provider) : IHandler<Query, Response>
    {
        public IRepositoryProvider provider = provider;

        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await provider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId);

            if(access.EndDate <= DateTime.Now)
            {
                response.Text = $"Ваша подписка закончилась {access.EndDate.ToLongDateString()} Чтобы продолжить использовать сервис, продлите подписку.";
                response.InlineKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("1 месяц за 100руб.", "payForMonth")
                    }
                });
            }
            else
            {
                response.Text = $"Ваша подписка действительна до {access.EndDate.ToLongDateString()}";
                response.InlineKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Получить QR код.", "getQrCode")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("1 месяц за 100руб.", "payForMonth")
                    }
                });
            }

            return response;
        }
    }
}
