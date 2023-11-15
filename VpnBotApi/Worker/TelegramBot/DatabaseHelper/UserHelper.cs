using Database;
using Microsoft.EntityFrameworkCore;
using Database.Model;

namespace VpnBotApi.Worker.TelegramBot.DatabaseHelper
{
    public class UserHelper
    {
        public static async Task<int> CreateUser(long telegramUserId, Context context)
        {
            //Пробуем найти пользователя по Telegram User Id
            var user = await context.Users.FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            //Если не находим - создаем
            if (user == null)
            {
                user = new User()
                {
                    TelegramUserId = telegramUserId,
                };

                await context.Users.AddAsync(user);

                await context.SaveChangesAsync();
            }

            return user.Id;
        }
    }
}
