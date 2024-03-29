using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.TegroPayment
{
    public class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            return true;
        }
    }
}
