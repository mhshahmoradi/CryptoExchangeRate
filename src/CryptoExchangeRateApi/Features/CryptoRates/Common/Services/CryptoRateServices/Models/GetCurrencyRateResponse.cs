using CryptoExchangeRateApi.Common.Validation;

namespace CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices.Models;

public class GetCurrencyRateResponse
{
    public string? Symbol { get; set; }
    public Dictionary<string, decimal?> Prices { get; set; } = new Dictionary<string, decimal?>();
    public ValidationError? Error { get; set; } 
    public bool HasError => Error != null;
}