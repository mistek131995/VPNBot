using Database.Model;
using QRCoder;

namespace VpnBotApi.Worker.TelegramBot.Common
{
    public class Helper
    {
        public static byte[] GetAccessQrCode(Access access)
        {
            var accessString = $"vless://{access.Guid}@{access.Ip}:{access.Port}?type={access.Network}&security={access.Security}&fp={access.Fingerprint}&pbk={access.PublicKey}&sni={access.ServerName}&sid={access.ShortId}&spx=%2F#{access.AccessName}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(accessString, QRCodeGenerator.ECCLevel.H);
            var qRCode = new PngByteQRCode(qrCodeData);
            return qRCode.GetGraphic(20);
        }
    }
}
