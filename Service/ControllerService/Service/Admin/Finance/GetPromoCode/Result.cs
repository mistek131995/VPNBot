namespace Service.ControllerService.Service.Admin.Finance.GetPromoCode
{
    public class Result
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public int UsageCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
