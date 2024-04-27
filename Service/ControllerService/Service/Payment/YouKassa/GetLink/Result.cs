using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.YouKassa.GetLink
{
    internal class Result
    {
        public string id { get; set; }
        public string status { get; set; }
        public Amount amount { get; set; }
        public string description { get; set; }
        public Recipient recipient { get; set; }
        public DateTime created_at { get; set; }
        public Confirmation confirmation { get; set; }
        public bool test { get; set; }
        public bool paid { get; set; }
        public bool refundable { get; set; }
        public Metadata metadata { get; set; }

        public class Amount
        {
            public string value { get; set; }
            public string currency { get; set; }
        }

        public class Confirmation
        {
            public string type { get; set; }
            public string confirmation_url { get; set; }
        }

        public class Metadata
        {
        }

        public class Recipient
        {
            public string account_id { get; set; }
            public string gateway_id { get; set; }
        }
    }
}
