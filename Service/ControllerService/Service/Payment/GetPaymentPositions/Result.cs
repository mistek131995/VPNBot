namespace Service.ControllerService.Service.Payment.GetPaymentPositions
{
    public class Result
    {
        public List<AccessPosition> AccessPositions { get; set; }

        public class AccessPosition
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
        }
    }
}
