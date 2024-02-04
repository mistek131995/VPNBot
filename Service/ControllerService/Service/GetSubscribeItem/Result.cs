namespace Service.ControllerService.Service.GetSubscribeItem
{
    public class Result
    {
        public _SubscribeItem SubscribeItem { get; set; }
        public decimal ReferralBalance { get; set; }

        public class _SubscribeItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Link { get; set; }
        }
    }
}
