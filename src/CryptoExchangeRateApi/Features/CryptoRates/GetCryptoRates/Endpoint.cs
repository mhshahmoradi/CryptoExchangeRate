using CryptoExchangeRateApi.Common.Extensions;
using CryptoExchangeRateApi.Features.CryptoRates.GetCryptoRates.Models;
using CryptoExchangeRateApi.Features.CryptoRates.GetCryptoRates.Validator;

namespace CryptoExchangeRateApi.Features.CryptoRates.GetCryptoRates;

public sealed class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapGet("/api/crypto-rates/{symbol:required}", async ([AsParameters] CryptoRateRequest request) =>
            {
                var result = await request.CryptoRateService.GetCryptoRatesAsync(request.Symbol, request.CancellationToken);
                return result.HasError ? Results.BadRequest(result) : Results.Ok(result);
            }).Validator<CryptoRateRequest>()
            .RequireRateLimiting("cryptoRateLimit");
    }
}