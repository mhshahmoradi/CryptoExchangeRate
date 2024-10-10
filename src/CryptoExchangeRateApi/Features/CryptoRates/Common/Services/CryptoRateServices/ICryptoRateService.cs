using CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices.Models;

namespace CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices;

public interface ICryptoRateService
{
    Task<IReadOnlyList<GetCurrencyRateResponse>> GetCryptoRatesAsync(string symbol, CancellationToken cancellationToken);
}