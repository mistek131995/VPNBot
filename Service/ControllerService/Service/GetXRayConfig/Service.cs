using Application.ControllerService.Common;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.GetXRayConfig
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var testConnections = new List<Result>()
            {
                new Result("%2F#subscribe-access-vpn2", "2.59.183.140", 443, "vless", "4a2cc04a-2184-4311-be9c-15216ac09461", "tcp", "reality", "K3aNlmMC_2WU39eLkUGcp4arcNpc8ze1aKTnpcS-tAc", "chrome", "yahoo.com", "ab2cc97b"),
                new Result("%2F#subscribe-access-vpn1", "163.5.207.114", 443, "vless", "96931db0-325b-45c4-b06d-4d23e4e0f54f", "tcp", "reality", "LCuiKV8N1EctocogPKiyjO5bkgd7xg_hHa6eGXwEBjU", "chrome", "ferzu.com", "0383b501"),
                new Result("%2F#subscribe-access-vpn3", "185.9.55.87", 443, "vless", "4a2cc04a-2184-4311-be9c-15216ac09461", "tcp", "reality", "K3aNlmMC_2WU39eLkUGcp4arcNpc8ze1aKTnpcS-tAc", "chrome", "yahoo.com", "ab2cc97b")
            };

            if (request.CountryId == 0)
            {

                var index = new Random().Next(0, 2);
                return testConnections.ElementAtOrDefault(index);

            }
            else if (request.CountryId == 1)
            {

                return testConnections[0];

            }
            else if (request.CountryId == 2)
            {
                return testConnections[1];
            }
            else
            {
                return testConnections[2];
            }
        }
    }
}
