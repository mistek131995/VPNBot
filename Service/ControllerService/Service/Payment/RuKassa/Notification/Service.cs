using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Service.ControllerService.Service.Payment.RuKassa.Notification
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var query = HttpUtility.ParseQueryString(request.Query);
            var id = query["id"];
            var createdDateTime = query["createdDateTime"];
            var amount = query["amount"];

            var signature = Signature.GenerateSignature($"{id}|{createdDateTime}|{amount}", "f1bcf17bb8a0a91966e6bb55b20e6761");

            Console.WriteLine(signature);
            Console.WriteLine(request.Signature);

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
