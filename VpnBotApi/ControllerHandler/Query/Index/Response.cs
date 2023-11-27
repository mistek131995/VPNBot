namespace VpnBotApi.ControllerHandler.Query.Index
{
    public class Response
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string RegisterDate { get; set; }
        public string EndAccessDate { get; set; }

        public List<Payment> Payments { get; set; }

        public class Payment
        {
            public int Id { get; set; }
            public string Range { get; set; }
            public string Date { get; set; }
            public decimal Price { get; set; }
        }
    }
}
