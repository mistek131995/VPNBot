using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.PayOk.Notification
{
    public class Request : IRequest<bool>
    {
        public string payment_id { get; set; }
        public int shop { get; set; }
        public float amount { get; set; }
        public string desc { get; set; }
        public string currency { get; set; }
        public string sign { get; set; }
    }
}
