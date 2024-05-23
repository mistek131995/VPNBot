using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service.ControllerService.Common
{
    public class ExchangeRateService
    {
        public record Currency(string CharCode, decimal Value);


        public DateTime UpdateDate { get; set; }
        public List<Currency> CurrencyList { get; set; } = new List<Currency>();

        public async Task UpdateCurrencyListAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://www.cbr-xml-daily.ru/daily_json.js");
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            var jsonUSD = JObject.Parse(body)["Valute"]["USD"].ToString();
            var usd = JsonConvert.DeserializeObject<Currency>(jsonUSD);
            CurrencyList.Add(usd);

            UpdateDate = DateTime.Now;
        }
    }
}
