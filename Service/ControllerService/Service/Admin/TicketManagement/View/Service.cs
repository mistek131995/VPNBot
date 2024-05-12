using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.TicketManagement.View
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var ticket = await repositoryProvider.TicketRepository.GetByIdAsync(request.TicketId) 
                ?? throw new HandledException("Тикет не найден.");

            var result = new Result();
            result.Id = ticket.Id;
            result.Title = ticket.Title;
            result.TicketMessages = ticket.TicketMessages.Select(m => new Result.TicketMessage()
            {
                Id = m.Id,
                Message = m.Message,
                SendDate = m.SendDate,
                UserId = m.UserId,
            }).ToList();

            return result;
        }
    }
}
