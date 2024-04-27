using Application.ControllerService.Common;
using Core.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.YouKassa.Notification
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {


            Console.WriteLine("------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(request));
            Console.WriteLine("------------------------");

            return true;
        }
    }
}
