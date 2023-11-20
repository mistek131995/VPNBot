using Database.Common;
using VpnBotApi.Worker.TelegramBot.Common;
using VpnBotApi.Worker.TelegramBot.WebClientRepository;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.ExtendForMonth
{
    public class Handler(IRepositoryProvider provider, TelegramBotWebClient webClient) : IHandler<Query, Response>
    {
        private readonly IRepositoryProvider provider = provider;
        private readonly TelegramBotWebClient webClient = webClient;
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await provider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                ?? throw new Exception("Ваш ползователь и доступ не найдены. Очистите чат с ботом и получите доступ.");

            if((access.EndDate - DateTime.Now).TotalDays > 2)
            {
                response.Text = $"Вы сможете продлить доступ {access.EndDate.AddDays(-1).ToString("dd.MM.yyyy")}. Воспользуйтесь текущим QR кодом.";

                response.AccessQrCode = Helper.GetAccessQrCode(access);
            }
            else
            {
                var user = await provider.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                    ?? throw new Exception("Ваш ползователь и доступ не найдены. Очистите чат с ботом и получите доступ.");

                access.EndDate = access.EndDate.AddMonths(1);

                await webClient.UpdateAccessDateAsync(access.Guid, query.TelegramUserId, access.EndDate);

                user.Access = access;

                await provider.UserRepository.UpdateAsync(user);

                response.Text = $"Ваш доступ продлен до {access.EndDate.ToString("dd.MM.yyyy")} (Настройки подключения не изменились, повторно сканировать QR код нет необходимости).";
                response.AccessQrCode = Helper.GetAccessQrCode(access);
            }

            return response;
        }
    }
}