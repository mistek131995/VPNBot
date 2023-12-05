using Core.Model.User;
using QRCoder;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(value));
            byte[] hash = mD5CryptoServiceProvider.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
