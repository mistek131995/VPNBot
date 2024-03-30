using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.RuKassa.CreateLink
{
    public class Request : IRequest<string>
    {
        public int AccessPositionId { get; set; }
        public int UserId { get; set; }
        public int ReferalAmount { get; set; }
    }
}
