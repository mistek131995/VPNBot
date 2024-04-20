using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using MD5Hash;
using Newtonsoft.Json;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.PayOk.CreateLink
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) ??
                throw new HandledExeption("Пользователь не найден");

            var lastPayment = user.Payments.FirstOrDefault();

            if (lastPayment != null && (DateTime.Now - lastPayment.Date).TotalMinutes < 10 && lastPayment.State == PaymentState.NotCompleted && user.Role == UserRole.User)
            {
                var minutes = (int)(10 - (DateTime.Now - lastPayment.Date).TotalMinutes);

                if (minutes > 0)
                    throw new HandledExeption($"Недавно вы уже создавали платеж, новый платеж можно создать через {minutes} минут(ы)");
                else
                    throw new HandledExeption($"Недавно вы уже создавали платеж, для создания нового платежа осталось подождать меньше минуты");
            }

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

            var queryDictionary = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(request.PromoCode))
            {
                var promoCode = await repositoryProvider.PromoCodeRepository.GetByCodeAsync(request.PromoCode) ??
                    throw new HandledExeption("Промокод не найден");

                if (user.UserUsedPromoCodes.Any(x => x.PromoCodeId == promoCode.Id))
                    throw new HandledExeption("Промокод уже использовался");

                var discount = accessPosition.Price * ((decimal)promoCode.Discount / 100);
                newPayment.Amount = (int)(accessPosition.Price - discount);

                queryDictionary.Add("custom", JsonConvert.SerializeObject(new { promoCode = request.PromoCode }));
            }

            queryDictionary.Add("amount", newPayment.Amount.ToString());

            user.Payments.Add(newPayment);

            user = await repositoryProvider.UserRepository.UpdateAsync(user);
            lastPayment = user.Payments.FirstOrDefault();

            queryDictionary.Add("payment", lastPayment.Id.ToString());
            queryDictionary.Add("currency", "RUB");
            queryDictionary.Add("shop", "11555");
            queryDictionary.Add("desc", $"Оплата счета - {lastPayment.Id}");

            var signString = $"{newPayment.Amount}|{lastPayment.Id}|11555|RUB|Оплата счета - {lastPayment.Id}|f035f6dde555705f6773f8f1a2ea5af7";
            var sign = signString.GetMD5();

            queryDictionary.Add("sign", sign);

            return $"https://payok.io/pay?{string.Join("&", queryDictionary.Select(x => $"{x.Key}={x.Value}"))}";
        }
    }
}
