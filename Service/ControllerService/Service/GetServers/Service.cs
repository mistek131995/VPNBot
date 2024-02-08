using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetServers
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            //result.Servers = (await repositoryProvider.VpnServerRepository.GetAllAsync()).Select(x => new Result.Server()
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Ip = x.Ip,
            //    Port = x.Port,
            //    UserCount = x.UserCount,
            //}).ToList();

            return result;
        }
    }
}
