using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.TegroPayment.GetLink
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
