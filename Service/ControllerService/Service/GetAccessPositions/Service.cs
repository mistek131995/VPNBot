using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetAccessPositions
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            result.AccessPositions = (await repositoryProvider.AccessPositionRepository.GetAllAsync()).Select(x => new Result.AccessPosition()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Link = $"https://pay.freekassa.ru/?m=45506&oa={x.Price}&currency=RUB&o={request.UserId}&s={MD5Hash.Hash.GetMD5($"45506:{x.Price}:B3/4d&cy&r__.Xk:RUB:{request.UserId}")}"
            }).ToList();

            return result;
        }
    }
}
