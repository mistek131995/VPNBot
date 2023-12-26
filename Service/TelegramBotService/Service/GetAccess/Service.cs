using Application.TelegramBotService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.HttpClientService;
using Service.ControllerService.Common;
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
                var vpnServers = await repositoryProvider.VpnServerRepository.GetAllAsync();

                user.Access = new Access()
                {
                    Guid = Guid.NewGuid(),
                    EndDate = DateTime.Now.AddDays(14)
                };

                user = await httpClient.CreateInboundUserAsync(user, vpnServers);

                await repositoryProvider.UserRepository.UpdateAsync(user);

                var vpnServer = await repositoryProvider.VpnServerRepository.GetByIdAsync(user.Access.VpnServerId);
                vpnServer.UserCount += 1;
                await repositoryProvider.VpnServerRepository.UpdateManyAsync(new List<Core.Model.VpnServer.VpnServer>() { vpnServer });

                result.QRCode = Helper.GetAccessQrCode(user.Access, vpnServer.Ip);
                result.Text = $"Получен пробный доступ сроком до {user.Access.EndDate.ToString("dd.MM.yyyy")}, скачайте приложение и загрузите QR код в приложение. " +
                    $"\nБот работает в тестовом режиме, дальнейшие продления бесплатны. " +
                    $"\nДля продления подписки, перейдите 'Аккаунт' -> 'Подписка' -> '1 месяц за 0руб.' ";
            }

            result.ReplyKeyboard = ButtonTemplate.GetMainMenuButton();

            return result;
        }
    }
}
