using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;
using System.Security.Cryptography;
using System.Text;

namespace Service.ControllerService.Service.Payment.RuKassa.Notification
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var signature = CalculateHmacSha256("f1bcf17bb8a0a91966e6bb55b20e6761", request.Query);

            Console.WriteLine(signature);
            Console.WriteLine(request.Signature);

            Console.WriteLine("---------------");
            Console.WriteLine(request.Query);

            return true;
        }

        public static string CalculateHmacSha256(string key, string message)
        {
            // Ключ и сообщение должны быть преобразованы в массив байтов
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            // Использование класса HMACSHA256 для вычисления HMAC
            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(messageBytes);

                // Преобразование результата из массива байтов в шестнадцатеричную строку
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.AppendFormat("{0:x2}", b);
                }
                return sb.ToString();
            }
        }
    }
}
