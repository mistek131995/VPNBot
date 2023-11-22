using Database.Common;
using Database.Model;
using VpnBotApi.Worker.TelegramBot.Common;
using VpnBotApi.Worker.TelegramBot.WebClientRepository;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.ExtendForMonth
{
    public class Handler(IRepositoryProvider repositoryProvides, TelegramBotWebClient webClient) : IHandler<Query, Response>
    {
        private readonly IRepositoryProvider repositoryProvides = repositoryProvides;
        private readonly TelegramBotWebClient webClient = webClient;
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await repositoryProvides.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                ?? throw new Exception("Ваш ползователь и доступ не найдены. Очистите чат с ботом и получите доступ.");

            if((access.EndDate - DateTime.Now).TotalDays > 2)
            {
                response.Text = $"Вы сможете продлить доступ {access.EndDate.AddDays(-1).ToString("dd.MM.yyyy")}. Воспользуйтесь текущим QR кодом.";

                response.AccessQrCode = Helper.GetAccessQrCode(access);
            }
            else
            {
                var user = await repositoryProvides.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                    ?? throw new Exception("Ваш ползователь и доступ не найдены. Очистите чат с ботом и получите доступ.");


                //Если доступ в БД устарел на более чем 7 дней, считаем его устаревшим
                //Удаляем его из БД и создаем новое подключение
                if((DateTime.Now - access.EndDate).TotalDays > 7)
                {
                    //Создаем новое подключение
                    var newAccess = await webClient.CreateNewAccess(query.TelegramUserId, DateTime.Now.AddMonths(1));

                    access.Ip = newAccess.Ip;
                    access.Port = newAccess.Port;
                    access.Network = newAccess.Network;
                    access.Security = newAccess.Security;
                    access.AccessName = newAccess.AccessName;
                    access.Guid = newAccess.Guid;
                    access.Fingerprint = newAccess.RealitySettings.Settings.Fingerprint;
                    access.PublicKey = newAccess.RealitySettings.Settings.PublicKey;
                    access.ServerName = newAccess.RealitySettings.ServerNames.First();
                    access.ShortId = newAccess.RealitySettings.ShortIds.First();
                    access.EndDate = newAccess.EndDate;

                    //Добавляем доступ к пользователю
                    user.Access = access;

                    //Обновляем пользователя
                    await repositoryProvides.UserRepository.UpdateAsync(user);

                    response.Text = $"Ваш доступ продлен до {access.EndDate.ToString("dd.MM.yyyy")} (Настройки подключения изменились, необходимо повторно загрузить QR код в приложение).";
                    response.AccessQrCode = Helper.GetAccessQrCode(user.Access);
                }
                else
                {
                    access.EndDate = access.EndDate.AddMonths(1);

                    await webClient.UpdateAccessDateAsync(query.TelegramUserId, access.EndDate);

                    user.Access = access;

                    await repositoryProvides.UserRepository.UpdateAsync(user);

                    response.Text = $"Ваш доступ продлен до {access.EndDate.ToString("dd.MM.yyyy")} (Настройки подключения не изменились, повторно сканировать QR код нет необходимости).";
                    response.AccessQrCode = Helper.GetAccessQrCode(access);
                }
            }

            return response;
        }
    }
}