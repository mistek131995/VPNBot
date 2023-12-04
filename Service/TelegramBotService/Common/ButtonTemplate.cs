using Core.Model.AccessPosition;
using Telegram.Bot.Types.ReplyMarkups;

namespace Service.TelegramBotService.Common
{
    public static class ButtonTemplate
    {
        /// <summary>
        /// Кнопка которая появляется после кнопки старт с предложение получить доступ
        /// </summary>
        /// <returns></returns>
        public static ReplyKeyboardMarkup GetAccessButton() => new ReplyKeyboardMarkup(new List<KeyboardButton[]>()
        {
            new KeyboardButton[]
            {
                new KeyboardButton("Получить доступ")
            }
        })
        { ResizeKeyboard = true };


        /// <summary>
        /// Основное меню
        /// </summary>
        public static ReplyKeyboardMarkup GetMainMenuButton() => new ReplyKeyboardMarkup(new List<KeyboardButton[]>()
        {
            new KeyboardButton[]
            {
                new KeyboardButton("Аккаунт")
            },
            new KeyboardButton[]
            {
                new KeyboardButton("Скачать приложение"),
                new KeyboardButton("Помощь")
            }
        })
        { ResizeKeyboard = true };


        /// <summary>
        /// Раздел помощи
        /// </summary>
        /// <returns></returns>
        public static InlineKeyboardMarkup GetHelpButton() => new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
        {
            new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithUrl("Сообщить об ошибке", "https://forms.yandex.ru/u/655ad4f0c417f36108ad28da/")
            },
            new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithCallbackData("Приложение Android", "helpAndroid"),
                InlineKeyboardButton.WithCallbackData("Приложение IOS", "helpIOS"),
            }
        });


        /// <summary>
        /// Ссылки на скачивание приложений
        /// </summary>
        /// <returns></returns>
        public static InlineKeyboardMarkup GetDownloadAppButton() => new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
        {
            new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithUrl("Android", "https://play.google.com/store/apps/details?id=com.v2ray.ang"),
                InlineKeyboardButton.WithCallbackData("Инструкция", "helpAndroid")
            },
            new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithUrl("IOS", "https://apps.apple.com/us/app/streisand/id6450534064"),
                InlineKeyboardButton.WithCallbackData("Инструкция", "helpIOS")
            }
        });


        /// <summary>
        /// Управление аккаунтом
        /// </summary>
        /// <param name="endDate">Дата окончания подписки, чтобы понять нужно ли выводить кнопку получения QR кода</param>
        /// <param name="domain">Домен сайта для ссылки на личный кабинет</param>
        /// <param name="telegramUserId">Id пользователя в телеграмм для ссылки на личный кабинет</param>
        /// <param name="guid">Guid дотсупа для ссылки на личный кабинет</param>
        /// <returns></returns>
        public static InlineKeyboardMarkup GetAccountManegment(DateTime endDate, string domain, long telegramUserId, Guid guid)
        {
            var inlineKeyboard = new List<InlineKeyboardButton[]>();

            //Если подписка не истекла
            if (endDate.Date > DateTime.Now.Date)
            {
                inlineKeyboard.Add([
                    InlineKeyboardButton.WithCallbackData("Получить QR код", "getQrCode"),
                ]);
            }

            //Общие кнопки 
            inlineKeyboard.Add([
                InlineKeyboardButton.WithUrl("Личный кабинет", $"http://{domain}/login?TelegramUserId={telegramUserId}&Guid={guid}"),
                InlineKeyboardButton.WithCallbackData("Подписка", "accessPositionList")
            ]);

            return new InlineKeyboardMarkup(inlineKeyboard); ;
        }

        /// <summary>
        /// Ссылки на скачивание приложений
        /// </summary>
        /// <returns></returns>
        public static InlineKeyboardMarkup GetDownloadButton() => new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
        {
            new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithUrl("Android", "https://play.google.com/store/apps/details?id=com.v2ray.ang"),
                InlineKeyboardButton.WithCallbackData("Инструкция", "helpAndroid")
            },
            new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithUrl("IOS", "https://apps.apple.com/us/app/streisand/id6450534064"),
                InlineKeyboardButton.WithCallbackData("Инструкция", "helpIOS")
            }
        });

        /// <summary>
        /// Список доступных подписок
        /// </summary>
        /// <param name="accessPositions"></param>
        /// <returns></returns>
        public static InlineKeyboardMarkup GetAccessPositionsButton(List<(AccessPosition position, string link)> accessPositions)
        {
            var inlineButtons = new List<InlineKeyboardButton[]>();

            foreach(var accessPosition in accessPositions)
            {
                if(accessPosition.position.Price == 0)
                {
                    inlineButtons.Add(new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData($"{accessPosition.position.Name} за {accessPosition.position.Price}руб.", "freeExtend")
                    });
                }
                else
                {
                    inlineButtons.Add(new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithUrl($"{accessPosition.position.Name} за {accessPosition.position.Price}руб.", accessPosition.link)
                    });
                }
            }

            return new InlineKeyboardMarkup(inlineButtons);
        }
    }
}
