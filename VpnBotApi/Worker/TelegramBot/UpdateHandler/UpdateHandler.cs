using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace VpnBotApi.Worker.TelegramBot.UpdateHandler
{
    internal class UpdateHandler(MessageHandler.MessageHandler messageHandler, CallbackQueryHandler.CallbackQueryHandler callbackQueryHandler, Serilog.ILogger logger)
    {

        public async Task HandlingAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            try
            {
                if (update.Type == UpdateType.Message)
                {
                    await messageHandler.HandlingAsync(client, update);
                }
                else if (update.Type == UpdateType.CallbackQuery)
                {
                    await callbackQueryHandler.HandlingAsync(client, update);
                }
            }
            catch (Exception ex)
            {
                var chatId = 0l;
                if (update.Type == UpdateType.Message)
                {
                    chatId = update.Message.From.Id;
                }
                else if (update.Type == UpdateType.CallbackQuery)
                {
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    await client.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
                }

                logger.Error($"В воркере телеграм бота произошла ошибка у пользователя {chatId}. \n {ex.Message}.");
                await client.SendTextMessageAsync(chatId, "Произошла ошибка, мы уже получили уведомление об этом и работаем на ее устранением. Попробуйте повторить попытку через некоторое время.");
            }
        }
    }
}
