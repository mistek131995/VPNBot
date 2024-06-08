using Core.Model.User;

namespace Service.ControllerService.Service.GetIndex
{
    public class Result
    {
        public Result()
        {
            Payments = new List<Payment>();
        }

        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? EndAccessDate { get; set; }
        public decimal Balance { get; set; }
        public string ReferralLink { get; set; }
        public bool IsExpired { get; set; }

        public List<Payment> Payments { get; set; }
        public List<Referral> Referrals { get; set; }

        public class Payment
        {
            public int Id { get; set; }
            public string Range { get; set; }
            public DateTime Date { get; set; }
            public string Price { get; set; }
            public PaymentState State { get; set; }
        }

        public class Referral
        {
            public string Login { get; set; }
            public decimal TopupTotal { get; set; }
        }
    }
}
