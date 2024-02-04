using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetAddTicketForm
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            result.Categories = (await repositoryProvider.TicketCategoryRepository.GetAllAsync())
                .Select(x => new Result.Option()
                {
                    Value = x.Id.ToString(),
                    Label = x.Name,
                })
                .ToList();

            return result;
        }
    }
}
