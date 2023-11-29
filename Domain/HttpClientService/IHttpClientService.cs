namespace Domain.HttpClientService
{
    public interface IHttpClientService
    {
        public Task<List<Guid>> DeleteInboundUserAsync(List<Guid> guids, string ip, int port, string userName, string password);
    }
}
