﻿using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Admin.FileManager.DeleteDirectory
{
    public class Request : IRequest<bool>
    {
        public string Path { get; set; }
    }
}
