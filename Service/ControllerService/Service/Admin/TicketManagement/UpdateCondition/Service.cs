using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.TicketManagement.UpdateCondition
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var ticket = await repositoryProvider.TicketRepository.GetByIdAsync(request.TicketId);
            ticket.Condition = request.Condition;
            await repositoryProvider.TicketRepository.UpdateAsync(ticket);

            return true;
        }
    }
}
