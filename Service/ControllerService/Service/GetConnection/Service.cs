using Application.ControllerService.Common;
using Core.Common;
using Infrastructure.HttpClientService;

namespace Service.ControllerService.Service.GetConnection
{
    internal class Service(IRepositoryProvider repositoryProvider, IHttpClientService httpClientService) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var testConnections = new List<Result>()
            {
                new Result()
                {
                    Name = "%2F#subscribe-access-vpn2",
                    Ip = "2.59.183.140",
                    Port = 443,
                    Network = "tcp",
                    Protocol = "vless",
                    Security = "reality",
                    PublicKey = "K3aNlmMC_2WU39eLkUGcp4arcNpc8ze1aKTnpcS-tAc",
                    Fingerprint = "chrome",
                    ServerName = "yahoo.com",
                    ShortId = "ab2cc97b",
                    Guid = "4a2cc04a-2184-4311-be9c-15216ac09461"
                },
                new Result()
                {
                    Name = "%2F#subscribe-access-vpn1",
                    Ip = "163.5.207.114",
                    Port = 443,
                    Network = "tcp",
                    Protocol = "vless",
                    Security = "reality",
                    PublicKey = "LCuiKV8N1EctocogPKiyjO5bkgd7xg_hHa6eGXwEBjU",
                    Fingerprint = "chrome",
                    ServerName = "ferzu.com",
                    ShortId = "0383b501",
                    Guid = "96931db0-325b-45c4-b06d-4d23e4e0f54f"
                },
                new Result()
                {
                    Name = "%2F#subscribe-access-vpn3",
                    Ip = "185.9.55.87",
                    Port = 443,
                    Network = "tcp",
                    Protocol = "vless",
                    Security = "reality",
                    PublicKey = "K3aNlmMC_2WU39eLkUGcp4arcNpc8ze1aKTnpcS-tAc",
                    Fingerprint = "chrome",
                    ServerName =  "yahoo.com",
                    ShortId = "ab2cc97b",
                    Guid = "4a2cc04a-2184-4311-be9c-15216ac09461"
                }
            };

            if(request.CountryId == 0)
            {

                var index = new Random().Next(0, 2);
                return testConnections.ElementAtOrDefault(index);

            }else if(request.CountryId == 1)
            {

                return testConnections.FirstOrDefault(x => x.Ip == "2.59.183.140");

            }else if (request.CountryId == 2)
            {
                return testConnections.FirstOrDefault(x => x.Ip == "163.5.207.114");
            }
            else
            {
                return testConnections.FirstOrDefault(x => x.Ip == "185.9.55.87");
            }
        }

    }
}
