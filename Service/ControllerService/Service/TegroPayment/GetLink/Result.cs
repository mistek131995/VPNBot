using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.TegroPayment.GetLink
{
    public class Result
    {
        public List<PaymentLink> Links { get; set; } = new List<PaymentLink>();

        public class PaymentLink
        {
            public string Title { get; set; }
            public string Link { get; set; }
        }
    }
}
