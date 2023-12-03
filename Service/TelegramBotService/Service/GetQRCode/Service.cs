using Application.TelegramBotService.Common;
using Core.Common;

namespace Service.TelegramBotService.Service.GetQRCode
{
    internal class Service(IRepositoryProvider repositoryProvider) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(request.TelegramUserId) 
                ?? throw new Exception("Пользователь не найден.");

            if(user.Access.EndDate.Date <= DateTime.Now.Date)
            {
                result.Text = "Ваш доступ устарел. Для получения нового доступа перейдите в 'Аккаунт' -> 'Подписка' и выберите новую подписку.";
            }
            else
            {
                var vpnServer = await repositoryProvider.VpnServerRepository.GetByIdAsync(user.Access.VpnServerId) 
                    ?? throw new Exception("VPN сервер не найден.");

                result.Text = $"Ваша подписка активна до {user.Access.EndDate.ToShortDateString()}.";
                result.QRCode = Helper.GetAccessQrCode(user.Access, vpnServer.Ip);
            }

            return result;
        }
    }
}
