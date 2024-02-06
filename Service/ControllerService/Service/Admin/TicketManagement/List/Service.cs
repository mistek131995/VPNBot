using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.TicketManagement.List
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var tickets = await repositoryProvider.TicketRepository.GetAllActiveAsync();

            var userIds = tickets.Select(x => x.UserId).Distinct().ToList();
            var users = await repositoryProvider.UserRepository.GetByIdsAsync(userIds);

            result.Tickets = tickets.Select(x => new Result.Ticket()
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.CategoryName,
                CreateDate = x.CreateDate,
                UserName = users.FirstOrDefault(u => u.Id == x.UserId)?.Login ?? "Пользователь не найден"
            }).ToList();

            return result;
        }
    }
}
