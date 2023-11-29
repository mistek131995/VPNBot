using Database.Common;
using Database.Model;
using VpnBotApi.Worker.TelegramBot.Common;
using VpnBotApi.Worker.TelegramBot.WebClientRepository;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.BuyAccess
{
    public class Handler(IRepositoryProvider repositoryProvider, TelegramBotWebClient webClientRepository) : IHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(query.AccessPositionId) 
                ?? throw new Exception("Выбранный период подписки не был найден.");

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                ?? throw new Exception("Пользователь не найден.");


            if(accessPosition.Price == 0) 
            {
                if(user.Access == null || (DateTime.Now - user.Access.EndDate).TotalDays > 7)
                {
                    var newInboundUser = await webClientRepository.CreateNewAccess(query.TelegramUserId, DateTime.Now.AddMonths(accessPosition.MonthCount));

                    var vpnServer = await repositoryProvider.VpnServerRepository.GetByIp(newInboundUser.Ip)
                        ?? throw new Exception("Не удалось получить VPN сервер, очистите чат и попробуйте снова.");

                    user.Access = new Access()
                    {
                        Port = newInboundUser.Port,
                        Network = newInboundUser.Network,
                        Security = newInboundUser.Security,
                        AccessName = newInboundUser.AccessName,
                        Guid = newInboundUser.Guid,
                        Fingerprint = newInboundUser.RealitySettings.Settings.Fingerprint,
                        PublicKey = newInboundUser.RealitySettings.Settings.PublicKey,
                        ServerName = newInboundUser.RealitySettings.ServerNames.First(),
                        ShortId = newInboundUser.RealitySettings.ShortIds.First(),
                        EndDate = newInboundUser.EndDate,
                        VpnServerId = vpnServer.Id
                    };

                    await repositoryProvider.UserRepository.UpdateAsync(user);

                    response.AccessQrCode = Helper.GetAccessQrCode(user.Access);
                    response.Text = $"Вам предоставлен новый доступ до {newInboundUser.EndDate.ToShortDateString()}. Скачайте приложение и загрузите в него полученный QR код.";
                }
                else if((user.Access.EndDate - DateTime.Now).TotalDays > 2)
                {
                    response.Text = $"Бесплатное продление станет доступным {user.Access.EndDate.AddDays(-2).ToShortDateString()}";
                }
                else if(user.Access.EndDate > DateTime.Now)
                {
                    await webClientRepository.UpdateAccessDateAsync(query.TelegramUserId, DateTime.Now.AddMonths(accessPosition.MonthCount));

                    user.Access.EndDate = DateTime.Now.AddMonths(accessPosition.MonthCount);
                    await repositoryProvider.UserRepository.UpdateAsync(user);
                }
            }
            else
            {
                response.Text = "Платное продление временно недоступно.";
            }

            return response;
        }
    }
}
