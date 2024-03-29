using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.TegroPayment.Notification
{
    public class Request : IRequest<bool>
    {
        public string shop_id { get; set; }
        public decimal amount { get; set; }
        public string order_id { get; set; }
        public string payment_system { get; set; }
        public string currency { get; set; }
        public int test { get; set; }
        public string sign { get; set; }
    }
}
