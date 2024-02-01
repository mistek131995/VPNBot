using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.CreateTicket
{
    public class Request : IRequest<int>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
}
