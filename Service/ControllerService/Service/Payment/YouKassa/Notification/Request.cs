using Application.ControllerService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.Payment.YouKassa.Notification
{
    public class Request : IRequest<bool>
    {
        public string type { get; set; }
        public string @event { get; set; }
        public Object @object { get; set; }

        public class Amount
        {
            public string value { get; set; }
            public string currency { get; set; }
        }

        public class IncomeAmount
        {
            public string value { get; set; }
            public string currency { get; set; }
        }

        public class Metadata
        {
        }

        public class Object
        {
            public string id { get; set; }
            public string status { get; set; }
            public Amount amount { get; set; }
            public IncomeAmount income_amount { get; set; }
            public string description { get; set; }
            public Recipient recipient { get; set; }
            public PaymentMethod payment_method { get; set; }
            public DateTime captured_at { get; set; }
            public DateTime created_at { get; set; }
            public bool test { get; set; }
            public RefundedAmount refunded_amount { get; set; }
            public bool paid { get; set; }
            public bool refundable { get; set; }
            public Metadata metadata { get; set; }
        }

        public class PaymentMethod
        {
            public string type { get; set; }
            public string id { get; set; }
            public bool saved { get; set; }
            public string title { get; set; }
            public string account_number { get; set; }
        }

        public class Recipient
        {
            public string account_id { get; set; }
            public string gateway_id { get; set; }
        }

        public class RefundedAmount
        {
            public string value { get; set; }
            public string currency { get; set; }
        }
    }
}
