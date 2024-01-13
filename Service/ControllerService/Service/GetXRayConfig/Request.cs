using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.GetXRayConfig
{
    public class Request : IRequest<Result>
    {
        public int CountryId { get; set; }
        public int UserId { get; set; }
    }
}
