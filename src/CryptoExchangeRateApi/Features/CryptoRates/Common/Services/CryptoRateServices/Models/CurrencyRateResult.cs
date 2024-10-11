namespace CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices.Models;

public sealed class CurrencyRateResult
{
    public string? Convert { get; set; }
    public decimal? Price { get; set; }
    public string? Error { get; set; }
}