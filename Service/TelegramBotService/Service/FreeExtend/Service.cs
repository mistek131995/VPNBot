﻿using Application.TelegramBotService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.HttpClientService;

namespace Service.TelegramBotService.Service.FreeExtend
{
    internal class Service(IRepositoryProvider repositoryProvider, IHttpClientService httpClientService) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(request.TelegramUserId);

            if ((user.Access.EndDate.Date - DateTime.Now.Date).TotalDays > 2)
            {
                result.Text = $"Бесплатное продление станет доступно {user.Access.EndDate.AddDays(-2).ToShortDateString()}.";
            }
            else if (user.Access.IsDeprecated)
            {
                //Тут генерируем новый доступ, потому что он был удален очисткой устаревших доступов
                var vpnServer = await repositoryProvider.VpnServerRepository.GetWithMinimalUserCountAsync();

                user.Access = new Access()
                {
                    Id = user.Access.Id,
                    Guid = Guid.NewGuid(),
                    EndDate = DateTime.Now.AddMonths(1),
                    VpnServerId = vpnServer.Id,
                };

                user = await httpClientService.CreateInboundUserAsync(user, vpnServer);

                await repositoryProvider.UserRepository.UpdateAsync(user);

                result.QRCode = Helper.GetAccessQrCode(user.Access, vpnServer.Ip);
                result.Text = $"Получен новый доступ сроком до {user.Access.EndDate.ToShortDateString()}, необходимо загрузить новый QR код в приложение.";
            }
            else
            {
                //Если доступ не был удален, обновляем старый доступ
                //Если дата окнчания меньше текущей даты (доступ истек) прибавляем есяц от текущей даты, если доступ еще активен, прибавляем к нему месяц
                var endDate = user.Access.EndDate.Date < DateTime.Now.Date ? DateTime.Now.AddMonths(1) : user.Access.EndDate.AddMonths(1);
                var vpnServer = await repositoryProvider.VpnServerRepository.GetByIdAsync(user.Access.VpnServerId);

                user.Access.EndDate = endDate;
                await httpClientService.UpdateInboundUserAsync(user, vpnServer);
                await repositoryProvider.UserRepository.UpdateAsync(user);

                result.Text = $"Ваш доступ был продлен на 1 месяц. Дата окончания доступа: {endDate.ToShortDateString()}.";
            }

            return result;
        }
    }
}
