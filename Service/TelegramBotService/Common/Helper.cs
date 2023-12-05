using Core.Model.User;
using QRCoder;
using System.Security.Cryptography;
using System.Text;

namespace Application.TelegramBotService.Common
{
    public class Helper
    {
        public static byte[] GetAccessQrCode(Access access, string ipAddress)
        {
            var accessString = $"vless://{access.Guid}@{ipAddress}:{access.Port}?type={access.Network}&security={access.Security}&fp={access.Fingerprint}&pbk={access.PublicKey}&sni={access.ServerName}&sid={access.ShortId}&spx=%2F#{access.AccessName}";

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(accessString, QRCodeGenerator.ECCLevel.H);
            var qRCode = new PngByteQRCode(qrCodeData);
            return qRCode.GetGraphic(20);
        }

        public static string GetMD5Hash(string value)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            return Convert.ToBase64String(hash);
        }
    }
}
