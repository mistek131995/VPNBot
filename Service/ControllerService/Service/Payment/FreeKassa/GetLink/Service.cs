using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;
using static Service.ControllerService.Service.Payment.FreeKassa.GetLink.Result;

namespace Service.ControllerService.Service.Payment.FreeKassa.GetLink
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();
            var subscribeItem = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.Id)
                ?? throw new HandledExeption("Подписка не найдена.");

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledExeption("Пользователь не найден.");

            if (user.Balance < request.Sale)
                request.Sale = user.Balance;

            var price = subscribeItem.Price - request.Sale;

            result.ReferralBalance = user.Balance;
            result.SubscribeItem = new _SubscribeItem()
            {
                Id = subscribeItem.Id,
                Name = subscribeItem.Name,
                Price = price,
                Link = $"https://pay.freekassa.ru/?m=45506&oa={price}&currency=RUB&o={request.UserId}&s={MD5Hash.Hash.GetMD5($"45506:{price}:B3/4d&cy&r__.Xk:RUB:{request.UserId}")}&us_position_id={subscribeItem.Id}&us_sale={request.Sale}"
            };

            return result;
        }
    }
}
