using Service.ControllerService.Common;

namespace Service.ControllerService.Service.GetSettings
{
    public class Result
    {
        public string CaptchaPublicKey { get; set; }
        public List<ExchangeRateService.Currency> CurrencyList { get; set; }
    }
}
