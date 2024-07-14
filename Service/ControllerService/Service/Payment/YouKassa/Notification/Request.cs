using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.YouKassa.Notification
{
    public class Request : IRequest<bool>
    {
        //public string type { get; set; }
        //public string @event { get; set; }
        public Object @object { get; set; }
        public string? IP {  get; set; }

        public class Object
        {
            public Guid id { get; set; }
            public string status { get; set; }
            //public Amount amount { get; set; }
            //public IncomeAmount income_amount { get; set; }
            //public string description { get; set; }
            //public Recipient recipient { get; set; }
            //public PaymentMethod payment_method { get; set; }
            //public DateTime captured_at { get; set; }
            //public DateTime created_at { get; set; }
            //public bool test { get; set; }
            //public RefundedAmount refunded_amount { get; set; }
            //public bool paid { get; set; }
            //public bool refundable { get; set; }
            //public string receipt_registration { get; set; }
            //public Metadata metadata { get; set; }
            //public AuthorizationDetails authorization_details { get; set; }
        }

        //// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        //public class Amount
        //{
        //    public string value { get; set; }
        //    public string currency { get; set; }
        //}

        //public class AuthorizationDetails
        //{
        //    public string rrn { get; set; }
        //    public string auth_code { get; set; }
        //    //public ThreeDSecure three_d_secure { get; set; }
        //}

        //public class IncomeAmount
        //{
        //    public string value { get; set; }
        //    public string currency { get; set; }
        //}

        //public class Metadata
        //{
        //}

        //public class PaymentMethod
        //{
        //    public string type { get; set; }
        //    public string id { get; set; }
        //    public bool saved { get; set; }
        //    public string title { get; set; }
        //    public string account_number { get; set; }
        //}

        //public class Recipient
        //{
        //    public string account_id { get; set; }
        //    public string gateway_id { get; set; }
        //}

        //public class RefundedAmount
        //{
        //    public string value { get; set; }
        //    public string currency { get; set; }
        //}

        //public class ThreeDSecure
        //{
        //    public bool applied { get; set; }
        //    public string protocol { get; set; }
        //    public bool method_completed { get; set; }
        //    public bool challenge_completed { get; set; }
        //}
    }
}
