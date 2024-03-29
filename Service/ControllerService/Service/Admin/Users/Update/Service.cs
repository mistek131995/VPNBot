using Application.ControllerService.Common;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Admin.Users.Update
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public Task<bool> HandlingAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
