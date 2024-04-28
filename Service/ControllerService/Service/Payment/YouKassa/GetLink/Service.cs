using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.ControllerService.Common;
using System.Net.Http.Headers;
using System.Text;

namespace Service.ControllerService.Service.Payment.YouKassa.GetLink
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) ??
                throw new HandledExeption("Пользователь не найден");

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.Id) ??
                throw new HandledExeption("Позиция не найдена");

            var promoCode = await repositoryProvider.PromoCodeRepository.GetByCodeAsync(request.PromoCode);

            if (promoCode != null && user.Payments.Any(x => x.PromoCodeId == promoCode.Id))
                throw new HandledExeption("Промокод уже использовался");

            var shopId = "376859"; // Замените на ваш Идентификатор магазина
            var secretKey = "live_PTUhuGnzhz3JoBqcgkqm8v_QoR3DLx61Zu4F1etyFug"; // Замените на ваш Секретный ключ
            //var shopId = "378461"; // Замените на ваш Идентификатор магазина
            //var secretKey = "test_OI5RhR_h07nXYWcDZEJa9c4_F_FbF3Gjv8mj8DNeIu8"; // Замените на ваш Секретный ключ

            var price = (decimal)accessPosition.Price;

            if (promoCode != null)
            {
                var discount = (decimal)promoCode.Discount / 100 * accessPosition.Price;
                price -= discount;
            }

            using (var client = new HttpClient())
            {
                var byteArray = Encoding.UTF8.GetBytes($"{shopId}:{secretKey}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Add("Idempotence-Key", Guid.NewGuid().ToString());

                var obj = new
                {
                    amount = new
                    {
                        value = price,
                        currency = "RUB"
                    },
                    capture = true,
                    receipt = new
                    {
                        customer = new
                        {
                            email = user.Email,
                            items = new[]
                            {
                                new
                                {
                                    description = $"Подписка LockVPN ({accessPosition.Name})",
                                    quantity = 1.ToString(),
                                    amount = new
                                    {
                                        value = price.ToString(),
                                        currency = "RUB"
                                    },
                                    vat_code = "1"
                                }
                            }
                        }
                    },
                    confirmation = new
                    {
                        type = "redirect",
                        return_url = "https://lockvpn.me/payment?status=success"
                    },
                    description = $"Подписка LockVPN ({accessPosition.Name})"
                };

                var jsonObj = JsonConvert.SerializeObject(obj);

                var buffer = Encoding.UTF8.GetBytes(jsonObj);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync($"https://api.yookassa.ru/v3/payments", byteContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Result>(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    var newPayment = new Core.Model.User.Payment()
                    {
                        AccessPositionId = accessPosition.Id,
                        Amount = accessPosition.Price,
                        Date = DateTime.Now,
                        State = PaymentState.NotCompleted,
                        UserId = request.UserId,
                        PromoCodeId = promoCode?.Id ?? 0,
                        Guid = Guid.Parse(result.id),
                    };

                    user.Payments.Add(newPayment);
                    user = await repositoryProvider.UserRepository.UpdateAsync(user);

                    return result.confirmation.confirmation_url;
                }

                throw new HandledExeption("Не удалось создать ссылку на оплату", true);
            }
        }
    }
}
