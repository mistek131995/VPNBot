namespace Service.ControllerService.Service.ReferralIndex
{
    public class Result
    {
        public string ReferralLink { get; set; }
        public List<Referral> Referrals { get; set; }

        public class Referral
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public decimal TopupByMonth { get; set; }
            public decimal TopupTotal { get; set; }
        }
    }
}
