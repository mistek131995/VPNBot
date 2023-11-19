using Database.Common;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.ExtendForWeek
{
    public class Handler(IRepositoryProvider provider) : IHandler<Query, Response>
    {
        private readonly IRepositoryProvider provider = provider;
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await provider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                ?? throw new UserOrAccessNotFountException("Ваш ползователь и доступ не найдены. Очистите чат с ботом и получите доступ.");

            if((access.EndDate - DateTime.Now).TotalDays > 2)
            {
                response.Text = $"Вы сможете продлить доступ {access.EndDate.AddDays(-1).ToString("dd.MM.yyyy")}. Воспользуйтесь текущим QR кодом.";

                response.AccessQrCode = Helper.GetAccessQrCode(access);
            }
            else
            {
                var user = await provider.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                    ?? throw new UserOrAccessNotFountException("Ваш ползователь и доступ не найдены. Очистите чат с ботом и получите доступ.");

                access.EndDate = access.EndDate.AddDays(7);

                user.Access = access;

                await provider.UserRepository.UpdateAsync(user);

                response.Text = $"Ваш доступ продлен до {access.EndDate.ToString("dd.MM.yyyy")}.";
                response.AccessQrCode = Helper.GetAccessQrCode(access);
            }

            return response;
        }
    }
}