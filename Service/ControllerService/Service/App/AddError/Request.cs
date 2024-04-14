using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.App.AddError
{
    public class Request : IRequest<bool>
    {
        public string? Location { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
