using CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices.Models;

namespace CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices;

public interface ICryptoRateService
{
    Task<GetCurrencyRateResponse> GetCryptoRatesAsync(string symbol, CancellationToken cancellationToken);
}