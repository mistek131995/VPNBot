using Application.TelegramBotService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.HttpClientService;
using System.Net.Http;

namespace Service.TelegramBotService.Service.FreeExtend
{
    internal class Service(IRepositoryProvider repositoryProvider, IHttpClientService httpClientService) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(request.TelegramUserId);

            if((user.Access.EndDate.Date - DateTime.Now.Date).TotalDays > 2)
            {
                result.Text = $"Бесплатное продление станет доступно {user.Access.EndDate.AddDays(-2).ToShortDateString()}.";
            }
            else if(user.Access.IsDeprecated)
            {
                //Тут генерируем новый доступ, потому что он был удален очисткой устаревших доступов
                var vpnServer = await repositoryProvider.VpnServerRepository.GetWithMinimalUserCountAsync();

                user.Access = new Access()
                {
                    Id = user.Access.Id,
                    Guid = Guid.NewGuid(),
                    EndDate = DateTime.Now.AddMonths(1),
                    VpnServerId = vpnServer.Id,
                };

                user = await httpClientService.CreateInboundUserAsync(user, vpnServer)
                    ?? throw new Exception("Ошибка при создании пользователя в VPN подключении.");

                await repositoryProvider.UserRepository.UpdateAsync(user);

                result.QRCode = Helper.GetAccessQrCode(user.Access, vpnServer.Ip);
                result.Text = $"Получен новый доступ сроком до {user.Access.EndDate.ToShortDateString()}, необходимо загрузить новый QR код в приложение.";
            }
            else
            {

            }

            return result;
        }
    }
}
