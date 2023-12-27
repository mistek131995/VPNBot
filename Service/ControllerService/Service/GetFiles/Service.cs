using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetFiles
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            result.Files = (await repositoryProvider.FileRepository.GetAllAsync()).Select(x => new Result.File()
            {
                Id = x.Id,
                Name = x.Name,
                Tag = x.Tag,
                Version = x.Version
            }).ToList();

            return result;
        }
    }
}
