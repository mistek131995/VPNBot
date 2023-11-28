using Database.Common;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Command.ChangePassword
{
    public class Handler(IRepositoryProvider provider) : IControllerHandler<Command, bool>
    {
        public async Task<bool> HandlingAsync(Command query)
        {
            var user = await provider.UserRepository.GetByIdAsync(query.UserId) 
                ?? throw new Exception("Пользователь не найден.");

            if (user.Password != query.OldPassword)
                throw new Exception("Введен неверный пароль.");

            user.Password = query.Password;

            await provider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
