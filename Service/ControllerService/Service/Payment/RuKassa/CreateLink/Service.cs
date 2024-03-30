using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.RuKassa.CreateLink
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledExeption("Пользователь не найден");

            if (user.Balance < request.ReferalAmount)
                throw new HandledExeption("Недостаточно средств на реферальном балансе");

            var lastPayment = user.Payments.LastOrDefault();

            if (lastPayment != null && (DateTime.Now - lastPayment.Date).TotalMinutes < 10)
                throw new HandledExeption($"Недавно вы уже создавали платеж, новый платеж можно создать через {(DateTime.Now - lastPayment.Date).TotalMinutes} минут(ы)");

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.AccessPositionId);

            user.Payments.Add(new Core.Model.User.Payment()
            {
                AccessPositionId = accessPosition.Id,
            });



            //var payment = new Core.Model.User.Payment()
            //{
            //    Guid = Guid.NewGuid(),
            //    AccessPositionId = request.AccessPositionId,
            //    Amount = accessPosition.Price - request.ReferalAmount,
            //};




            //var queryDictionary = new Dictionary<string, string>
            //{
            //    { "shop_id", "2087" },
            //    { "order_id", "0" },
            //    { "token", "f1bcf17bb8a0a91966e6bb55b20e6761" },
            //    { "amount", "10" },
            //    { "data", Newtonsoft.Json.JsonConvert.SerializeObject(payment.Guid) }
            //};

            //var query = string.Join("&", queryDictionary.Select(x => $"{x.Key}={x.Value}"));

            //var httpClient = new HttpClient();
            //var response = await httpClient.GetAsync($"https://lk.rukassa.pro/api/v1/create?{query}");

            //if (response.IsSuccessStatusCode)
            //{
            //    var responseString = await response.Content.ReadAsStringAsync();
            //}

            return string.Empty;
        }
    }
}
