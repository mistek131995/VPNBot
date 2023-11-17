using Telegram.Bot;
using Telegram.Bot.Exceptions;
using VpnBotApi.Worker.TelegramBot.Common;
using VpnBotApi.Worker.TelegramBot.Handler.ErrorHandler;

namespace VPNBot.Handler.ErrorHandler
{
    internal class ErrorHandler
    {
        public async Task HandlingAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            // Тут создадим переменную, в которую поместим код ошибки и её сообщение 
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return;
        }
    }
}
