namespace CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices.Models;

public sealed class CryptoRateResponse
{
    public Status Status { get; set; }
    public Dictionary<string, PriceData>? Data { get; set; }
}

public sealed class Status
{
    public DateTime Timestamp { get; set; }
    public int ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public int Elapsed { get; set; }
    public int CreditCount { get; set; }
}

public sealed class PriceData
{
    public Dictionary<string, Quote>? Quote { get; set; }
}

public sealed class Quote
{
    public decimal Price { get; set; }
}

