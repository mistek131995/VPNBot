using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Ticket;
using Infrastructure.MailService;
using Infrastructure.TelegramService;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Ticket.AddMessage
{
    internal class Service(IRepositoryProvider repositoryProvider, ITelegramNotificationService telegramNotificationService) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            if (request.FormFiles.Any(x => x.Length > 2097152))
                throw new HandledException("Размер файла не должен превышать 2мб");

            var ticket = await repositoryProvider.TicketRepository.GetByTicketIdAndUserIdAsync(request.TicketId, request.UserId);
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();
            var admins = await repositoryProvider.UserRepository.GetAllAdminsAsync();

            var newMessage = new TicketMessage(request.UserId, request.Message, TicketMessageCondition.New, new List<MessageFile>());

            foreach (var file in request.FormFiles)
            {
                var fileStream = file.OpenReadStream();
                var bytes = new byte[file.Length];
                fileStream.Read(bytes, 0, (int)file.Length);
                //files.Add(bytes);

                var filePath = $"/home/build/wwwroot/files/tickets/{request.TicketId}/{Guid.NewGuid()}.{file.FileName.Split(".")[1]}";

                await File.WriteAllBytesAsync(filePath, bytes);
                newMessage.AddFile(new MessageFile(file.FileName, filePath));
            }

            ticket.TicketMessages.Add(newMessage);

            await repositoryProvider.TicketRepository.UpdateAsync(ticket);


            //Тут оповещение админа о новом сообщении от пользователя
            var adminUsers = await repositoryProvider.UserRepository.GetAllAdminsAsync();
            var adminEmails = adminUsers
                .Select(x => x.Email)
                .ToList();

            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(adminEmails, $"Новое сообщение в тикете {ticket.Id} ({ticket.Title})", @$"
                В тикет {ticket.Id} пришло новое сообщение.</br>
                ----</br>
                {request.Message}</br>
                ----</br>
                </br>
                <a href='https://lockvpn.me/admin/ticket/{ticket.Id}'>Перейти к тикету</a>
            ");

            foreach (var admin in adminUsers)
            {
                if (admin.TelegramUserId == 0)
                    continue;

                await telegramNotificationService
                    .AddText($"В тикет {ticket.Id} пришло новое сообщение:\n\n{request.Message}")
                    .AddButtonWithLink("Перейти в тикет", $"https://lockvpn.me/admin/ticket/{ticket.Id}")
                    .SendNotificationAsync(admin.TelegramUserId);
            }

            return true;
        }
    }
}
