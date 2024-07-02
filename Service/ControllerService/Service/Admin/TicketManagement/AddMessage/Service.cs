using Application.ControllerService.Common;
using Core.Common;
using Infrastructure.MailService;
using Infrastructure.TelegramService;

namespace Service.ControllerService.Service.Admin.TicketManagement.AddMessage
{
    internal class Service(IRepositoryProvider repositoryProvider, ITelegramNotificationService telegramNotificationService) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var ticket = await repositoryProvider.TicketRepository.GetByIdAsync(request.TicketId);
            ticket.TicketMessages.Add(new Core.Model.Ticket.TicketMessage()
            {
                Message = request.Message,
                Condition = Core.Model.Ticket.TicketMessageCondition.New,
                SendDate = DateTime.Now,
                UserId = request.UsertId
            });

            await repositoryProvider.TicketRepository.UpdateAsync(ticket);

            //Оповещение пользователя о новом сообщении от админа
            var user = await repositoryProvider.UserRepository.GetByIdAsync(ticket.UserId);

            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(user.Email, $"Новое сообщение в тикете {ticket.Id} ({ticket.Title})", $@"
                    В тикет {ticket.Id} пришло новое сообщение.</br>
                    ----</br>
                    {request.Message}</br>
                    ----</br>
                    </br>
                    <a href='https://lockvpn.me/ticket/{ticket.Id}'>Перейти к тикету</a>
                ");

            if (user.TelegramChatId > 0)
                await telegramNotificationService
                    .AddText($"В тикет {ticket.Id} пришло новое сообщение.\n----\n{request.Message}\n----")
                    .SendNotificationAsync(user.TelegramChatId);

            return true;
        }
    }
}
