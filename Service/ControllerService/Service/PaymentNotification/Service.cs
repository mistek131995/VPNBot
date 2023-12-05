using Application.ControllerService.Common;

namespace Service.ControllerService.Service.PaymentNotification
{
    internal class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var sign = $"{request.MERCHANT_ID}:{request.AMOUNT}:-V9V(-Bb}}UXdAB}}:{request.MERCHANT_ORDER_ID}";

            if (sign == request.SIGN)
            {
                Console.WriteLine("Прошло успешно");
                return true;
            }
            Console.WriteLine("Ошибка");
            return false;
        }
    }
}
