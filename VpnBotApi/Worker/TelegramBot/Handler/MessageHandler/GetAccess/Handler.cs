﻿using Database.Common;
using Database.Model;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;
using VpnBotApi.Worker.TelegramBot.WebClientRepository;

namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.GetAccess
{
    public class Handler(IRepositoryProvider provider, TelegramBotWebClient webClient) : IHandler<Query, Response>
    {
        private readonly IRepositoryProvider provider = provider;
        private readonly TelegramBotWebClient webClient = webClient;
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await provider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId);

            response.ReplyKeyboard = new ReplyKeyboardMarkup(new List<KeyboardButton[]>()
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("Управление подпиской")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("Скачать приложение"),
                    new KeyboardButton("Сообщить об ошибке")
                }
            })
            {
                ResizeKeyboard = true,
            };

            if (access == null)
            {
                var user = await provider.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId);
                var newAccess = await webClient.CreateNewAccess(query.TelegramUserId, DateTime.Now.AddDays(7));

                user.Access = new Access()
                {
                    Ip = newAccess.Ip,
                    Port = newAccess.Port,
                    Network = newAccess.Network,
                    Security = newAccess.Security,
                    AccessName = newAccess.AccessName,
                    Guid = newAccess.Guid,
                    Fingerprint = newAccess.RealitySettings.Settings.Fingerprint,
                    PublicKey = newAccess.RealitySettings.Settings.PublicKey,
                    ServerName = newAccess.RealitySettings.ServerNames.First(),
                    ShortId = newAccess.RealitySettings.ShortIds.First(),
                    EndDate = newAccess.EndDate,
                };

                await provider.UserRepository.UpdateAsync(user);

                response.Text = $"Получен тестовый доступ до {user.Access.EndDate.ToShortDateString()}. Скачайте приложение и отсканируйте QR код. " +
                    $"На данный момент бот работает в тестовом режиме, вы можете продлять подписку бесплатно на 7 дней.";
                response.AccessQrCode = Helper.GetAccessQrCode(user.Access);
            }
            else if (access.EndDate.Date <= DateTime.Now.Date)
            {
                response.ReplyKeyboard = new ReplyKeyboardMarkup(new List<KeyboardButton[]>()
                {
                    new KeyboardButton[]
                    {
                        new KeyboardButton("Продлить подписку")
                    },
                    new KeyboardButton[]
                    {
                        new KeyboardButton("Скачать приложение"),
                        new KeyboardButton("Сообщить об ошибке")
                    }
                })
                {
                    ResizeKeyboard = true,
                };

                response.Text = "Действие подписки закончилось, для продолжения продлите доступ.";
            }
            else
            {
                response.Text = $"Доступ активен до {access.EndDate.ToShortDateString()}. Скачайте приложение и отсканируйте QR код.";
                response.AccessQrCode = Helper.GetAccessQrCode(access);
            }

            return response;
        }
    }
}
