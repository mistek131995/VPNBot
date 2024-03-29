using Application.ControllerService.Common;
using Core.Common;
using MD5Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Service.ControllerService.Service.TegroPayment.GetLink
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var shopId = "2CC61D57E60E499ED923D2367209CB8D";
            var secret = "5dO9c6LF";
            var apiKey = "cDh4QUvwb8pF0cBz";

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);
            var subscribePositions = await repositoryProvider.AccessPositionRepository.GetAllAsync();

            foreach(var position in subscribePositions)
            {
                var signDictionary = new Dictionary<string, string>
                {
                    { "shop_id", shopId },
                    { "amount", position.Price.ToString() },
                    { "currency", "RUB" },
                    { "order_id", request.UserId.ToString() }
                };

                if (user.Role == Core.Model.User.UserRole.Admin)
                    signDictionary.Add("test", "1");

                signDictionary = signDictionary
                    .OrderBy(x => x.Key)
                    .ToDictionary();

                var queryParameters = string.Join("&", signDictionary.Select(x => $"{x.Key}={x.Value}"));
                var sign = $"{queryParameters}{secret}".GetMD5();

                result.Links.Add(new Result.PaymentLink()
                {
                    Title = position.Name,
                    Link = "https://tegro.money/pay/?" + queryParameters + $"&sign={sign}"
                });
            }

            return result;
        }
    }
}
