using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace VPNBot.Handler.ErrorHandler
{
    internal class ErrorHandler
    {
        public static async Task HandlingAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
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
