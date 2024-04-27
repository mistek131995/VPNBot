using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.YouKassa.GetLink
{
    public class Request : IRequest<string>
    {
        public int UserId { get; set; }
        public int Id { get; set; }

        public string? PromoCode { get; set; }
    }
}
