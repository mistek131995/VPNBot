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
            var newTicket = new Core.Model.Ticket.Ticket()
            {
                Title = request.Title,
                CategoryId = request.CategoryId,
                Condition = TicketCondition.Open,
                CreateDate = DateTime.Now,
                UserId = request.UserId,
                TicketMessages = new List<TicketMessage>()
            };

            newTicket.TicketMessages.Add(new TicketMessage
            {
                Message = request.Message,
                Condition = TicketMessageCondition.New,
                SendDate = DateTime.Now,
                UserId = request.UserId,
            });

            var ticketId =  await repositoryProvider.TicketRepository.AddAsync(newTicket);

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
