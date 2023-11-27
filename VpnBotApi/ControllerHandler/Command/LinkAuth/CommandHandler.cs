using Database.Common;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Command.LinkAuth
{
    public class CommandHandler(IRepositoryProvider repositoryProvider) : IControllerHandler<Command, string>
    {
        public async Task<string> HandlingAsync(Command query)
        {
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAndAccessGuidAsync(query.TelegramUserId, query.AccessGuid) 
                ?? throw new Exception("Пользователь не найден.");

            if (user.Password != query.Password)
                throw new Exception("Введен неверный пароль.");

            return "Вы вошли в аккаунт.";
        }
    }
}
