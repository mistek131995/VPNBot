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

        public class AuthorizationDetails
        {
            public string rrn { get; set; }
            public string auth_code { get; set; }
            public ThreeDSecure three_d_secure { get; set; }
        }

        public class Card
        {
            public string first6 { get; set; }
            public string last4 { get; set; }
            public string expiry_month { get; set; }
            public string expiry_year { get; set; }
            public string card_type { get; set; }
            public string issuer_country { get; set; }
            public string issuer_name { get; set; }
        }

        public class Metadata
        {
        }

        public class Object
        {
            public string id { get; set; }
            public string status { get; set; }
            public bool paid { get; set; }
            public Amount amount { get; set; }
            public AuthorizationDetails authorization_details { get; set; }
            public DateTime created_at { get; set; }
            public string description { get; set; }
            public DateTime expires_at { get; set; }
            public Metadata metadata { get; set; }
            public PaymentMethod payment_method { get; set; }
            public bool refundable { get; set; }
            public bool test { get; set; }
        }

        public class PaymentMethod
        {
            public string type { get; set; }
            public string id { get; set; }
            public bool saved { get; set; }
            public Card card { get; set; }
            public string title { get; set; }
        }

        public class ThreeDSecure
        {
            public bool applied { get; set; }
        }

    }
}
