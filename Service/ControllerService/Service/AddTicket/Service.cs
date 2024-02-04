using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Support;

namespace Service.ControllerService.Service.AddTicket
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, int>
    {
        public async Task<int> HandlingAsync(Request request)
        {
            var newTicket = new Ticket()
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

            return await repositoryProvider.TicketRepository.AddAsync(newTicket);
        }
    }
}
