using Microsoft.AspNetCore.Mvc;

namespace CryptoExchangeRateApi.Features.CryptoRates.GetCryptoRates.Models;

public record CryptoRateRequest([FromRoute] string Symbol,[FromServices] ICryptoRateService CryptoRateService, CancellationToken CancellationToken);