using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.Common;
using Newtonsoft.Json;
using Service.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Service.ControllerService.Service.Payment.CryptoCloud.GetLink.Result;

namespace Service.ControllerService.Service.Payment.CryptoCloud.GetLink
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) ??
            throw new HandledException("Пользователь не найден");

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.Id) ??
            throw new HandledException("Позиция не найдена");

            var promoCode = await repositoryProvider.PromoCodeRepository.GetByCodeAsync(request.PromoCode);

            if (promoCode != null && user.Payments.Any(x => x.PromoCodeId == promoCode.Id))
                throw new HandledException("Промокод уже использовался");

            var price = accessPosition.Price;

            if (promoCode != null)
            {
                var discount = (decimal)promoCode.Discount / 100 * accessPosition.Price;
                price -= (int)discount;
            }

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Token eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1dWlkIjoiTWpBNU1UYz0iLCJ0eXBlIjoicHJvamVjdCIsInYiOiJmNGJmY2IzZTdhNzMwZWU3NGEwZGExODkzMjE4MGFkNjRkY2JhMjRlNzc4MzQxN2UyZTc3YTU1MDBkODE4NmQ4IiwiZXhwIjo4ODExMzk2MDExNn0.AiZKPg3SNRQOIwrNIjsNvdSyTsUY6hoJCJj2v9Uk3b8");

            var orderGuid = Guid.NewGuid();

            var obj = new
            {
                shop_id = "0zc04Vo96SI49MQ2",
                amount = price,
                currency = "RUB",
                order_id = orderGuid,
                email = user.Email,
            };

            var jsonObj = JsonConvert.SerializeObject(obj);
            var buffer = Encoding.UTF8.GetBytes(jsonObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://api.cryptocloud.plus/v2/invoice/create", byteContent);

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Result>(resultString);

                var newPayment = new Core.Model.User.Payment()
                {
                    AccessPositionId = accessPosition.Id,
                    Amount = accessPosition.Price,
                    Date = DateTime.Now,
                    State = PaymentState.NotCompleted,
                    UserId = request.UserId,
                    PromoCodeId = promoCode?.Id ?? 0,
                    Guid = orderGuid,
                    PaymentMethod = PaymentMethod.CryptoCloud
                };

                user.Payments.Add(newPayment);
                await repositoryProvider.UserRepository.UpdateAsync(user);

                return result.result.link;
            }

            throw new HandledException("Не удалось создать ссылку на оплату", true);
        }
    }
}
