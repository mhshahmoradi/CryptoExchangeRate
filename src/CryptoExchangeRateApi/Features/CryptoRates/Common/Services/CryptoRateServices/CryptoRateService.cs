using System.Collections.Concurrent;
using CryptoExchangeRateApi.Common.Validation;
using CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices.Models;
using CryptoExchangeRateApi.Infrastructure.Services.ApiKeyServices;

namespace CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices;

public sealed class CryptoRateService : ICryptoRateService
{
    private readonly IReadOnlyList<string> _converts = new[] { "USD", "EUR", "BRL", "GBP", "AUD" };
    private readonly HttpClient _httpClient;
    private readonly IApiKeyService _apiKeyService;
    private const string ApiUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest";

    public CryptoRateService(IHttpClientFactory httpClientFactory, IApiKeyService apiKeyService)
    {
        _httpClient = httpClientFactory.CreateClient("CryptoClient");
        _apiKeyService = apiKeyService;
    }

    public async Task<IReadOnlyList<GetCurrencyRateResponse>> GetCryptoRatesAsync(string symbol, CancellationToken cancellationToken)
    {
        var tasks = _converts.Select(async convert =>
        {
            var apiKey = _apiKeyService.GetNextApiKey();
            var builder = new UriBuilder(ApiUrl);
            var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            query["symbol"] = symbol;
            query["convert"] = convert;
            builder.Query = query.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
            request.Headers.Add("X-CMC_PRO_API_KEY", apiKey);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return new GetCurrencyRateResponse
                {
                    Error = ValidationError.Failure("Request failed.")
                };
            }

            var data = await response.Content.ReadFromJsonAsync<CryptoRateResponse>(cancellationToken);
            if (data.Status.ErrorMessage is not null)
            {
                return new GetCurrencyRateResponse
                {
                    Error = ValidationError.Failure(data.Status.ErrorCode, data.Status.ErrorMessage)
                };
            }

            return new GetCurrencyRateResponse
            {
                Response = data
            };
        });
        
        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }
}