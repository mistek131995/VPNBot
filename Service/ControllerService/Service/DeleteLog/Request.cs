using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.DeleteLog
{
    public class Request : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
