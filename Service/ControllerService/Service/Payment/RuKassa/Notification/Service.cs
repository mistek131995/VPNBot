using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Newtonsoft.Json;
using Service.ControllerService.Common;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Service.ControllerService.Service.Payment.RuKassa.Notification
{

    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        private record Data(string PromoCode);

        public async Task<bool> HandlingAsync(Request request)
        {
            var query = HttpUtility.ParseQueryString(request.Query);
            var id = query["id"];
            var orderId = query["order_id"];
            var createdDateTime = query["createdDateTime"];
            var amount = query["amount"];
            var inAmount = query["in_amount"];

            var signature = Signature.GenerateSignature($"{id}|{createdDateTime}|{amount}", "f1bcf17bb8a0a91966e6bb55b20e6761");

            if (signature == request.Signature && amount == inAmount)
            {
                var user = await repositoryProvider.UserRepository.GetByPaymentId(int.Parse(orderId))
                    ?? throw new Exception($"Не удалось найти пользователя по orderId (PaymentId) - {orderId}");

                var payment = user.Payments.FirstOrDefault(x => x.Id == int.Parse(orderId))
                    ?? throw new Exception($"Не удалось найти платеж по orderId (PaymentId) - {orderId}");

                if (payment.State == PaymentState.Completed)
                    throw new Exception("Попытка повторного зачисления по уже оплаченному счета");

                var paymentPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(payment.AccessPositionId)
                    ?? throw new Exception($"Не удалось найти подписку с Id - {payment.AccessPositionId}");

                if (user.AccessEndDate == null || user.AccessEndDate < DateTime.Now)
                    user.AccessEndDate = DateTime.Now.AddMonths(paymentPosition.MonthCount);
                else
                    user.AccessEndDate = user.AccessEndDate?.AddMonths(paymentPosition.MonthCount);

                payment.State = PaymentState.Completed;

                var dataJsonString = query["data"];

                if(!string.IsNullOrEmpty(dataJsonString))
                {
                    var data = JsonConvert.DeserializeObject<Data>(dataJsonString);

                    if (!string.IsNullOrEmpty(data.PromoCode))
                    {
                        var promoCode = await repositoryProvider.PromoCodeRepository.GetByCodeAsync(data.PromoCode)
                            ?? throw new Exception("Промокод не найден");

                        user.UserUsedPromoCodes.Add(new UserUsedPromoCode()
                        {
                            PromoCodeId = promoCode.Id,
                            UserId = user.Id
                        });
                    }
                }

                await repositoryProvider.UserRepository.UpdateAsync(user);

                return true;
            }

            throw new Exception("Ошибка подтверждения платежа");
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
