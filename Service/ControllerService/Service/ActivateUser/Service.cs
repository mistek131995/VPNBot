using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.ActivateUser
{
    internal class Service : IControllerService<Request, bool>
    {
        public Task<bool> HandlingAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
