namespace VpnBotApi.ControllerHandler.Query.Index
{
    public class Response
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime EndAccessDate { get; set; }

        public List<Payment> Payments { get; set; }

        public class Payment
        {
            public int Id { get; set; }
            public string Range { get; set; }
            public DateTime Date { get; set; }
            public string Price { get; set; }
        }
    }
}
