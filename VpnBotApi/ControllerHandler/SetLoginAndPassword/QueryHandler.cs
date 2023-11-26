using Database.Common;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.SetLoginAndPassword
{
    public class QueryHandler(IRepositoryProvider repositoryProvider) : IControllerHandler<Query, bool>
    {
        public async Task<bool> HandlingAsync(Query query)
        {
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAndAccessGuidAsync(query.TelegramUserId, query.AccessGuid) 
                ?? throw new Exception("Пользователь не найден.");

            if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
                throw new Exception("Логин и пароль уже установлены.");

            user.Login = query.Login;
            user.Password = query.Password;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
