using Application.ControllerService.Common;
using Core.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetProxyConnection
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        private record Inbound(int id, string remark, int port, string settings);
        private record Account(string user, string pass);

        public async Task<Result> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) ??
                throw new HandledException("Пользователь не найден");

            user.UpdateLastConnectionDate(DateTime.Now);

            if (user.AccessEndDate == null || user.AccessEndDate < DateTime.Now)
                throw new HandledException("Ваша подписка истекла");

            var location = await repositoryProvider.LocationRepository.GetByServerIpAsync(request.Ip) ??
                throw new HandledException("Локация не найдена не найден");

            var server = location.VpnServers.FirstOrDefault(x => x.Ip == request.Ip) ??
                throw new HandledException("Сервер не найден");

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var httpHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpHandler);
            httpClient.BaseAddress = new Uri($"http://{server.Ip}:{server.Port}");
            var authData = new MultipartFormDataContent
                {
                    { new StringContent(server.UserName), "username" },
                    { new StringContent(server.Password), "password" }
                };

            var authResponse = await httpClient.PostAsync("/login", authData);

            if (!authResponse.IsSuccessStatusCode)
                throw new HandledException("Произошла ошибка или сервер временно недоступен", true);

            var authContent = await authResponse.Content.ReadAsStringAsync();
            var authResult = bool.Parse(JObject.Parse(authContent)["success"].ToString());

            if (!authResult)
                throw new HandledException("Ошибка авторизации на сервере", true);

            var inboundsResponse = await httpClient.GetAsync("/panel/api/inbounds/list");

            if (!inboundsResponse.IsSuccessStatusCode)
                throw new HandledException("Не удалось получить список подключений");

            var inboundsString = await inboundsResponse.Content.ReadAsStringAsync();

            var inbounds = JsonConvert.DeserializeObject<List<Inbound>>(JObject.Parse(inboundsString)["obj"].ToString());

            var proxy = inbounds.FirstOrDefault(x => x.remark == "proxy") ?? 
                throw new HandledException("Прокси не найдено");

            var accounts = JsonConvert.DeserializeObject<List<Account>>(JObject.Parse(proxy.settings)["accounts"].ToString()).FirstOrDefault() ?? 
                throw new HandledException("Пользователь для прокси не найден");

            return new Result()
            {
                Ip = request.Ip,
                Port = proxy.port,
                Login = accounts.user,
                Password = accounts.pass
            };
        }
    }
}
