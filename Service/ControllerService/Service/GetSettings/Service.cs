using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.GetSettings
{
    internal class Service(IRepositoryProvider repositoryProvider, ExchangeRateService exchangeRateService) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result()
                ;
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            if (exchangeRateService.UpdateDate == DateTime.MinValue || (DateTime.Now - exchangeRateService.UpdateDate).TotalHours > 1)
                await exchangeRateService.UpdateCurrencyListAsync();

            result.CaptchaPublicKey = settings.CaptchaPublicKey;
            result.CurrencyList = exchangeRateService.CurrencyList;

            return result;
        }
    }
}
