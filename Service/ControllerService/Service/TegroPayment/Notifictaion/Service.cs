using Application.ControllerService.Common;
using MD5Hash;

namespace Service.ControllerService.Service.TegroPayment.Notification
{
    public class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var shopId = "2CC61D57E60E499ED923D2367209CB8D";
            var secret = "5dO9c6LF";

            var signDictionary = new Dictionary<string, string>
                {
                    { "shop_id", shopId },
                    { "amount", request.amount.ToString() },
                    { "currency", "RUB" },
                    { "order_id", request.order_id }
                };

            if (request.test != 0)
                signDictionary.Add("test", "1");

            var queryParameters = string.Join("&", signDictionary.Select(x => $"{x.Key}={x.Value}"));
            var sign = $"{queryParameters}{secret}".GetMD5();

            if (request.sign != sign)
                return false;

            return true;
        }
    }
}
