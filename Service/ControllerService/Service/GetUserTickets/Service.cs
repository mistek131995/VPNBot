using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetUserTickets
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();
            result.Tickets = (await repositoryProvider.TicketRepository.GetByUserIdAsync(request.UserId)).Select(x => new Result.Ticket()
            {
                 Id = x.Id,
                 Title = x.Title,
                 CreateDate = x.CreateDate,
                 Condition = x.Condition
            }).ToList();

            return result;
        }
    }
}
