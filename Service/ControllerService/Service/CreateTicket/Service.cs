﻿using Application.ControllerService.Common;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.CreateTicket
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, int>
    {
        public Task<int> HandlingAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
