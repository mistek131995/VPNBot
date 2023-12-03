namespace Infrastructure.HttpClientService.Model
{
    internal class InboundUser
    {
        public Guid Id { get; set; }
        public string Flow { get; set; }
        public string Email { get; set; }
        public int LimitIp { get; set; }
        public int TotalGB { get; set; }
        public long ExpiryTime { get; set; }
        public bool Enable { get; set; }
    }
}
