using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.TicketManagement.AddMessage
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
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

            return true;
        }
    }
}
