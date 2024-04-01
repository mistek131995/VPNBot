using Application.ControllerService.Common;
using Core.Common;
using Encrypt.Library;
using Service.ControllerService.Common;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace Service.ControllerService.Service.Payment.RuKassa.Notification
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var signature = SHAUtil.GetSHA256("id=6717318&order_id=22&amount=200.00&in_amount=200.00&data=&createdDateTime=2024-03-31T04%3A57%3A36%2B00%3A00&status=PAID", "f1bcf17bb8a0a91966e6bb55b20e6761");

            Console.WriteLine(request.Query == "id=6717318&order_id=22&amount=200.00&in_amount=200.00&data=&createdDateTime=2024-03-31T04%3A57%3A36%2B00%3A00&status=PAID");

            Console.WriteLine(signature);
            Console.WriteLine(request.Signature);
            Signature.GenerateSignature("id=6717318&order_id=22&amount=200.00&in_amount=200.00&data=&createdDateTime=2024-03-31T04%3A57%3A36%2B00%3A00&status=PAID", "f1bcf17bb8a0a91966e6bb55b20e6761");

            Console.WriteLine("---------------");
            Console.WriteLine(request.Query);

            return true;
        }

        public static string ComputeHmacSha256(string secretKey, string message)
        {
            // Convert the secret key and message to byte arrays
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            // Initialize the HMACSHA256 instance with the secret key
            using (HMACSHA256 hmacSha256 = new HMACSHA256(keyBytes))
            {
                // Compute the hash
                byte[] hashBytes = hmacSha256.ComputeHash(messageBytes);

                // Convert the hash to a hexadecimal string
                StringBuilder hex = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                return hex.ToString();
            }
        }

        public static string ComputeHmacSha256Hex(string secretKey, string message)
        {
            // Convert the secret key and message to byte arrays
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            // Initialize the HMACSHA256 instance with the secret key
            using (HMACSHA256 hmacSha256 = new HMACSHA256(keyBytes))
            {
                // Compute the hash
                byte[] hashBytes = hmacSha256.ComputeHash(messageBytes);

                // Convert the hash to a hexadecimal string
                StringBuilder hex = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                return hex.ToString();
            }
        }
    }
}
