using Application.TelegramBotService.Common;
using Core.Common;
using Core.Model.User;
using Service.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.Start
{
    internal class Service(IRepositoryProvider repositoryProvider) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(request.TelegramUserId);

            //Если пользователь не найден или у пользователя нет доступа
            //отображаем кнопку получения доступа
            if (user == null || user.Access == null)
            {
                await CreateUserAsync(request.TelegramUserId, request.TelegramChatId);

                result.Text = "У вас нет активного доступа. Вы можете получить пробный доступ сроком на 14 дней, для этого воспользуйтесь кнопкой ниже.";
                result.ReplyKeyboard = ButtonTemplate.GetAccessButton();
            }
            else
            {
                //Если доступ был найден и еще активен
                if(user.Access.EndDate > DateTime.Now) 
                {
                    var vpnServer = await repositoryProvider.VpnServerRepository.GetByIdAsync(user.Access.VpnServerId);

                    result.Text = "У вас есть активный доступ. Для использования VPN скачайте приложение и загрузите в него QR код.";
                    result.QRCode = Helper.GetAccessQrCode(user.Access, vpnServer.Ip);
                }
                else
                {
                    result.Text = "Ваш доступ устарел. Для получения нового доступа перейдите в 'Аккаунт' -> 'Подписка' и выберите новую подписку.";
                }

                result.ReplyKeyboard = ButtonTemplate.GetMainMenuButton();
            }

            return result;
        }

        private async Task CreateUserAsync(long telegramUserId, long telegramChatId)
        {
            await repositoryProvider.UserRepository.AddAsync(new User()
            {
                TelegramUserId = telegramUserId,
                TelegramChatId = telegramChatId,
                RegisterDate = DateTime.Now,
                Role = UserRole.User
            });
        }
    }
}
