using System.Security.Cryptography;
using System.Text;

namespace Service.ControllerService.Common
{
    public class Signature
    {
        public static string GenerateSignature(string serializeData, string secret)
        {
            if (string.IsNullOrEmpty(serializeData))
            {
                return null;
            }

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(serializeData);
            byte[] key = encoding.GetBytes(secret);

            using (var hash = new HMACSHA256(key))
            {
                var hashmessage = hash.ComputeHash(data);
                StringBuilder sbinary = new StringBuilder();

                for (int i = 0; i < hashmessage.Length; i++)
                {
                    sbinary.Append(hashmessage[i].ToString("x2"));
                }

                return sbinary.ToString();
            }
        }
    }
}
