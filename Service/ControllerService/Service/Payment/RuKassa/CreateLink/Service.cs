using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Newtonsoft.Json;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.RuKassa.CreateLink
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledExeption("Пользователь не найден");

            if (user.Role != UserRole.Admin)
                throw new HandledExeption("Ведутся технические работы, попробуйте позднее");

            var lastPayment = user.Payments.FirstOrDefault();

            if (lastPayment != null && (DateTime.Now - lastPayment.Date).TotalMinutes < 10 && lastPayment.State == PaymentState.NotCompleted)
            {
                var minutes = (int)(10 - (DateTime.Now - lastPayment.Date).TotalMinutes);

                if (minutes > 0)
                    throw new HandledExeption($"Недавно вы уже создавали платеж, новый платеж можно создать через {minutes} минут(ы)");
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
            lastPayment = user.Payments.FirstOrDefault();

            var queryDictionary = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(request.PromoCode))
            {
                var promoCode = await repositoryProvider.PromoCodeRepository.GetByCodeAsync(request.PromoCode) ??
                    throw new HandledExeption("Промокод не найден");

                if (user.UserUserPromoCodes.Any(x => x.PromoCodeId == promoCode.Id))
                    throw new HandledExeption("Промокод уже использовался");

                var discount = accessPosition.Price * ((decimal)promoCode.Discount / 100);
                accessPosition.Price = (int)(accessPosition.Price - discount);

                queryDictionary.Add("data", JsonConvert.SerializeObject(new { promoCode = request.PromoCode }));
            }

            queryDictionary.Add("shop_id", "2087");
            queryDictionary.Add("order_id", lastPayment.Id.ToString());
            queryDictionary.Add("token", "f1bcf17bb8a0a91966e6bb55b20e6761");
            queryDictionary.Add("amount", accessPosition.Price.ToString());

            var query = string.Join("&", queryDictionary.Select(x => $"{x.Key}={x.Value}"));

            //var httpClient = new HttpClient();
            //var response = await httpClient.GetAsync($"https://lk.rukassa.pro/api/v1/create?{query}");

            //if (response.IsSuccessStatusCode)
            //{
            //    var responseString = await response.Content.ReadAsStringAsync();
            //    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(responseString);

            //    return result.url;
            //}

            throw new HandledExeption("Не удалось получить ссылку на оплату. Попробуйте позднее.");
        }
    }
}
