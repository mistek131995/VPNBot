using Database;
using QRCoder;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQuery.GetNewAccess
{
    public static class NewAccessService
    {
        public static async Task<MessageModel> GetNewAccess(long telegramUserId, Context context)
        {
            //var accesses = await AccessHelper.GetAccessesByTelegramUserId(telegramUserId, context);

            //if (accesses.Count >= 1)
            //    return new MessageModel("У вас уже есть активный доступ к VPN, используйте его.", null);

            return new MessageModel("Доступ предоставлен. Отсканируйте QR код в приложении.", null);
        }
    }
}
