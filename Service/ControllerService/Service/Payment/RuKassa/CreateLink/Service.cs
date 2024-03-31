using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;
using Core.Model.User;

namespace Service.ControllerService.Service.Payment.RuKassa.CreateLink
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledExeption("Пользователь не найден");

            var lastPayment = user.Payments.LastOrDefault();

            if (lastPayment != null && (DateTime.Now - lastPayment.Date).TotalMinutes < 10 && lastPayment.State == PaymentState.NotCompleted)
            {
                var minutes = (int)(DateTime.Now - lastPayment.Date).TotalMinutes;

                if(minutes > 0)
                    throw new HandledExeption($"Недавно вы уже создавали платеж, новый платеж можно создать через {(int)(DateTime.Now - lastPayment.Date).TotalMinutes} минут(ы)");
                else
                    throw new HandledExeption($"Недавно вы уже создавали платеж, для создания нового платежа осталось подождать меньше минуты");
            }


            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.Id)
                ?? throw new HandledExeption("Не найдена позиция для оплаты");

            user.Payments.Add(new Core.Model.User.Payment()
            {
                AccessPositionId = accessPosition.Id,
                Amount = accessPosition.Price,
                Date = DateTime.Now,
                State = PaymentState.NotCompleted,
                UserId = request.UserId
            });

            user = await repositoryProvider.UserRepository.UpdateAsync(user);
            lastPayment = user.Payments.LastOrDefault();

            var queryDictionary = new Dictionary<string, string>
            {
                { "shop_id", "2087" },
                { "order_id", lastPayment.Id.ToString() },
                { "token", "f1bcf17bb8a0a91966e6bb55b20e6761" },
                { "amount", accessPosition.Price.ToString() }
            };

            var query = string.Join("&", queryDictionary.Select(x => $"{x.Key}={x.Value}"));

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://lk.rukassa.pro/api/v1/create?{query}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(responseString);

                return result.url;
            }

            throw new HandledExeption("Не удалось получить ссылку на оплату. Попробуйте позднее.");
        }
    }
}
