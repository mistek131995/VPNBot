using Application.ControllerService.Common;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.App.GetConnectionScreen
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);

            result.IsExpired = user.AccessEndDate?.Date < DateTime.Now.Date;
            result.AccessEndDate = user.AccessEndDate?.ToString("dd.MM.yyyy");

            return result;
        }
    }
}
