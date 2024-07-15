using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Ticket;
using Infrastructure.MailService;

namespace Service.ControllerService.Service.AddTicket
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, int>
    {
        public async Task<int> HandlingAsync(Request request)
        {
            var newTicket = new Core.Model.Ticket.Ticket(request.Title, request.CategoryId, DateTime.Now, TicketCondition.Open, request.UserId);
            newTicket.AddMessage(new TicketMessage(request.UserId, request.Message, TicketMessageCondition.New, new List<MessageFile>()));

            var ticketId =  await repositoryProvider.TicketRepository.AddAsync(newTicket);

            if (!Directory.Exists($"/home/build/wwwroot/files/tickets/{ticketId}/"))
                Directory.CreateDirectory($"/home/build/wwwroot/files/tickets/{ticketId}/");

            //Тут оповещение админа о новом тикете
            var adminUsers = await repositoryProvider.UserRepository.GetAllAdminsAsync();
            var adminEmails = adminUsers
                .Select(x => x.Email)
                .ToList();

            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(adminEmails, $"Новый тикет {ticketId} ({newTicket.Title})", @$"
                Создан новый тикет {ticketId} с темой {newTicket.Title}.</br>
                ----</br>
                {request.Message}</br>
                ----</br>
                </br>
                <a href='https://lockvpn.me/admin/ticket/{ticketId}'>Перейти к тикету</a>
            ");

            return ticketId;
        }
    }
}
