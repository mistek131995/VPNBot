using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Ticket;
using Infrastructure.MailService;

namespace Service.ControllerService.Service.Ticket.AddMessage
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var ticket = await repositoryProvider.TicketRepository.GetByTicketIdAndUserIdAsync(request.TicketId, request.UserId);

            ticket.TicketMessages.Add(new TicketMessage()
            {
                Condition = TicketMessageCondition.New,
                Message = request.Message,
                SendDate = DateTime.Now,
                UserId = request.UserId,
            });

            await repositoryProvider.TicketRepository.UpdateAsync(ticket);

            var mailService = new MailService(repositoryProvider);
            //Если пишет не владелец тикета, значит сообщение от админа и нужно увдеомить владельца тикета
            if (ticket.UserId != request.UserId)
            {
                var user = await repositoryProvider.UserRepository.GetByIdAsync(ticket.UserId);
                await mailService.SendEmailAsync(user.Email, $"Новое сообщение в тикете {ticket.Id}", $@"
                    В тикет {ticket.Id} пришло новое сообщение.</br>
                    ----</br>
                    {request.Message}</br>
                    ----</br>
                    </br>
                    <a href='https://lockvpn.me/ticket/{ticket.Id}'>Перейти к тикету</a>
                ");
            }
            else // Если сообщение от владельца тикета, значит отправляем админам
            {
                var adminUsers = await repositoryProvider.UserRepository.GetAllAdmins();
                var adminEmails = adminUsers
                    .Select(x => x.Email)
                    .ToList();

                await mailService.SendEmailAsync(adminEmails, $"Новое сообщение в тикете {ticket.Id}", @$"
                    В тикет {ticket.Id} пришло новое сообщение.</br>
                    ----</br>
                    {request.Message}</br>
                    ----</br>
                    </br>
                    <a href='https://lockvpn.me/admin/ticket/{ticket.Id}'>Перейти к тикету</a>
                ");
            }

            return true;
        }
    }
}
