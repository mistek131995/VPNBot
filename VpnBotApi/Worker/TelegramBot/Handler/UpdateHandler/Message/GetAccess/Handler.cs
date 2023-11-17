using Database.Common;
using Database.Model;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.GetAccess
{
    public class Handler(IRepositoryProvider provider) : IHandler<Query, Response>
    {
        private readonly IRepositoryProvider provider = provider;
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await provider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId);

            if (access == null)
            {
                var user = await provider.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId);
                user.Access = new Access()
                {
                    EndDate = DateTime.Now.AddDays(7)
                };

                await provider.UserRepository.UpdateAsync(user);

                response.Text = $"Получен тестовый доступ до {user.Access.EndDate.ToLongDateString()}. Скачайте приложение и отсканируйте QR код.";
            }
            else if(access.EndDate.Date >= DateTime.Now.Date)
            {
                //Предлагаем продлить доступ
            }
            else
            {
                //Выдаем действующий доступ с возможностью добавить IP адреса
            }

            return response;
        }
    }
}
