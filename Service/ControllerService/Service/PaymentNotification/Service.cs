using Application.ControllerService.Common;
using Application.TelegramBotService.Common;
using Core.Common;
using Core.Model.User;

namespace Service.ControllerService.Service.PaymentNotification
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var sign = Helper.GetMD5Hash($"{request.MERCHANT_ID}:{request.AMOUNT}:-V9V(-Bb}}UXdAB}}:{request.MERCHANT_ORDER_ID}");

            Console.WriteLine("Test");

            if (sign == request.SIGN)
            {
                //var user = await repositoryProvider.UserRepository.GetByIdAsync(request.MERCHANT_ORDER_ID);
                //var accessPosition = await repositoryProvider.AccessPositionRepository.GetByPriceAsync(request.AMOUNT);

                //user.Payments.Add(new Payment()
                //{
                //    AccessPositionId = accessPosition.Id,
                //    Date = DateTime.Now,
                //    UserId = user.Id,
                //});

                //await repositoryProvider.UserRepository.UpdateAsync(user);

                return true;
            }

            return false;
        }
    }
}
