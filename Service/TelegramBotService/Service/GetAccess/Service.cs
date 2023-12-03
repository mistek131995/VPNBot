using Application.TelegramBotService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.HttpClientService;
using Service.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.GetAccess
{
    internal class Service(IRepositoryProvider repositoryProvider, IHttpClientService httpClient) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(request.TelegramUserId)
                ?? throw new Exception("Пользователь не найден, очистите чат и попробуйте снова.");

            if (user.Access != null)
            {
                if (user.Access.EndDate.Date > DateTime.Now)
                {
                    var vpnServer = await repositoryProvider.VpnServerRepository.GetByIdAsync(user.Access.VpnServerId)
                        ?? throw new Exception("Неудалось найти VPN сервер.");

                    result.Text = $"У вас есть активная подписка сроком до {user.Access.EndDate.ToShortDateString()}, скачайте приложение и загрузите QR код в приложение.";
                    result.QRCode = Helper.GetAccessQrCode(user.Access, vpnServer.Ip);
                }
                else
                {
                    result.Text = "Ваш доступ устарел. Для получения нового доступа перейдите в 'Аккаунт' -> 'Подписка' и выберите новую подписку.";
                }
            }
            else
            {
                var vpnServer = await repositoryProvider.VpnServerRepository.GetWithMinimalUserCountAsync() 
                    ?? throw new Exception("Не удалось получить VPN сервер, очистите чат и попробуйте снова.");

                user.Access = new Access()
                {
                    Guid = Guid.NewGuid(),
                    EndDate = DateTime.Now.AddDays(14),
                    VpnServerId = vpnServer.Id,
                };

                user = await httpClient.CreateInboundUserAsync(user, vpnServer) 
                    ?? throw new Exception("Ошибка при создании пользователя в VPN подключении.");

                await repositoryProvider.UserRepository.UpdateAsync(user);

                result.QRCode = Helper.GetAccessQrCode(user.Access, vpnServer.Ip);
                result.Text = $"Получен пробный доступ сроком до {user.Access.EndDate.ToShortDateString()}, скачайте приложение и загрузите QR код в приложение.";
            }

            result.ReplyKeyboard = ButtonTemplate.GetMainMenuButton();

            return result;
        }
    }
}
