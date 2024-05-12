﻿using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Admin.FileManager.UploadFile
{
    internal class Request : IRequest<bool>
    {
        public string Path { get; set; }
        public byte[] Data { get; set; }
    }
}
