using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Ticket;

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

            return true;
        }
    }
}
