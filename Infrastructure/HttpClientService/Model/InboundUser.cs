namespace Infrastructure.HttpClientService.Model
{
    public class InboundUser
    {
        public Guid Id { get; set; }
        public string Flow { get; set; }
        public string Email { get; set; }
        public int LimitIp { get; set; }
        public int TotalGB { get; set; }
        public long ExpiryTime { get; set; }
        public bool Enable { get; set; }
        public string TgId { get; set; }
        public string SubId { get; set; }
        public int Reset { get; set; }
    }
}
