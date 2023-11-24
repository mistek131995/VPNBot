using Database.Common;
using Database.Model;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;
using VpnBotApi.Worker.TelegramBot.WebClientRepository;

namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.GetAccess
{
    public class Handler(IRepositoryProvider repositoryProvider, TelegramBotWebClient webClient) : IHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await repositoryProvider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId);

            if (access != null) 
            {
                if(access.EndDate.Date < DateTime.Now.Date)
                {
                    response.Text = $"Ваша подписка закончилась {access.EndDate.ToShortDateString()}. Для продления, перейдите в аккаунт и выберите срок новой подписки.";
                }
                else
                {
                    response.Text = $"У вас есть активная подписка сроком до {access.EndDate.ToShortDateString()}, скачайте приложение и загрузите QR код в приложение.";
                    response.AccessQrCode = Helper.GetAccessQrCode(access);
                }
            }
            else
            {
                var newInboundUser = await webClient.CreateNewAccess(query.TelegramUserId, DateTime.Now.AddDays(7));

                var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId) 
                    ?? throw new Exception("Пользователь не найден, очистите чат и попробуйте снова.");

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

                response.Text = $"Получен пробный доступ сроком до {access.EndDate.ToShortDateString()}, скачайте приложение и загрузите QR код в приложение.";
            }

            //Кнопки основного меню
            response.ReplyKeyboard = new ReplyKeyboardMarkup(new List<KeyboardButton[]>
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("Аккаунт")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("Скачать приложение")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("Помощь")
                }
            })
            { 
                ResizeKeyboard = true
            };

            return response;
        }
    }
}
