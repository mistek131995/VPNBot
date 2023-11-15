using Database;
using QRCoder;
using VpnBotApi.Worker.TelegramBot.DatabaseHelper;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.CallbackQuery.GetNewAccess
{
    public static class NewAccessService
    {
        public static async Task<MessageModel> GetNewAccess(long telegramUserId, Context context)
        {
            var accesses = await AccessHelper.GetAccessesByTelegramUserId(telegramUserId, context);

            if (accesses.Count >= 1)
                return new MessageModel("У вас уже есть активный доступ к VPN, используйте его.", null);

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode("asdasdasd", QRCodeGenerator.ECCLevel.H);
            var qRCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qRCode.GetGraphic(20);

            return new MessageModel("Доступ предоставлен. Отсканируйте QR код в приложении.", qrCodeBytes);
        }
    }
}
