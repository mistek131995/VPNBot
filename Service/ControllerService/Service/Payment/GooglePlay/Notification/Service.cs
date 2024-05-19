using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.GooglePlay.Notification
{
    internal class Service : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            return true;
        }
    }
}
