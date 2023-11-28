using Database.Common;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Command.SetLoginAndPassword
{
    public class CommandHandler(IRepositoryProvider repositoryProvider) : IControllerHandler<Command, bool>
    {
        public async Task<bool> HandlingAsync(Command query)
        {
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAndAccessGuidAsync(query.TelegramUserId, query.AccessGuid)
                ?? throw new Exception("Пользователь не найден.");

            if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
                throw new Exception("Логин и пароль уже установлены.");

            user.Login = query.Login;
            user.Password = query.Password;

            if(user.RegisterDate == DateTime.MinValue)
                user.RegisterDate = DateTime.Now;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
