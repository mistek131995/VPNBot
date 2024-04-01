using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Service.ControllerService.Service.Payment.Lava.CreateLink
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            //Получаем позицию для оплаты
            var paymentPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.Id) 
                ?? throw new HandledExeption("Не найдена позиция для оплаты");

            //Получаем пользователя
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) 
                ?? throw new HandledExeption("Пользователь не найден");

            //Создаем новый платеж
            user.Payments.Add(new Core.Model.User.Payment()
            {
                AccessPositionId = paymentPosition.Id,
                Amount = paymentPosition.Price,
                Date = DateTime.Now,
                State = Core.Model.User.PaymentState.NotCompleted,
                UserId = request.UserId,
                //Signature = string.Empty,
            });

            //Сохраняем платеж, чтобы получить Id
            user = await repositoryProvider.UserRepository.UpdateAsync(user);
            var lastPayment = user.Payments.LastOrDefault();

            //Создаем запрос с Id платежа и получаем сигнатуру
            var query = new
            {
                sum = paymentPosition.Price,
                orderId = lastPayment.Id,
                shopId = "00be473d-254f-4e40-a5ab-a7fd5db26fcf"
            };
            var serializeQuery = Newtonsoft.Json.JsonConvert.SerializeObject(query);
            var signature = Signature.GenerateSignature(serializeQuery, "30e27bc2cba9cb964ae0e86243058cc90c4e9d62");

            //Запрашиваем ссылку на оплату
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Signature", signature);
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.lava.ru/business/invoice/create", content);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(body);

                //Сохраняем сигнатуру платежа для будущей проерки на подленность
                lastPayment.Guid = Guid.Parse(result.data.id);
                await repositoryProvider.UserRepository.UpdateAsync(user);

                return result.data.url;
            }

            throw new HandledExeption("Не удалось получить ссылку на оплату. Попробуйте позднее.");
        }
    }
}
