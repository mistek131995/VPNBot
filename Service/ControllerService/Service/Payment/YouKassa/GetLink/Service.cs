using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Newtonsoft.Json;
using Service.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

            var newPayment = new Core.Model.User.Payment()
            {
                AccessPositionId = accessPosition.Id,
                Amount = accessPosition.Price,
                Date = DateTime.Now,
                State = PaymentState.NotCompleted,
                UserId = request.UserId
            };

            user.Payments.Add(newPayment);
            user = await repositoryProvider.UserRepository.UpdateAsync(user);
            var lastPayment = user.Payments.FirstOrDefault();

            var paymentId = lastPayment.Id; // Замените на ваш payment_id
            var shopId = "378461"; // Замените на ваш Идентификатор магазина
            var secretKey = "test_OI5RhR_h07nXYWcDZEJa9c4_F_FbF3Gjv8mj8DNeIu8"; // Замените на ваш Секретный ключ

            using (var client = new HttpClient())
            {
                var byteArray = Encoding.UTF8.GetBytes($"{shopId}:{secretKey}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Add("Idempotence-Key", Guid.NewGuid().ToString());

                var obj = new
                {
                    amount = new
                    {
                        value = 100,
                        currency = "RUB"
                    },
                    capture = true,
                    confirmation = new
                    {
                        type = "redirect",
                        return_url = "https://lockvpn.me/payment?status=success"
                    },
                    description = "Test"
                };

                var jsonObj = JsonConvert.SerializeObject(obj);

                var buffer = Encoding.UTF8.GetBytes(jsonObj);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync($"https://api.yookassa.ru/v3/payments", byteContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Result>(responseContent);

                return result.confirmation.confirmation_url;
            }
        }
    }
}
