using System.Collections.Concurrent;
using CryptoExchangeRateApi.Common.Validation;
using CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices.Models;
using CryptoExchangeRateApi.Infrastructure.Services.ApiKeyServices;

namespace CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices;

public sealed class CryptoRateService(IHttpClientFactory httpClientFactory, IApiKeyService apiKeyService)
    : ICryptoRateService
{
    private readonly IReadOnlyList<string> _converts = new[] { "USD", "EUR", "BRL", "GBP", "AUD" };
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly IApiKeyService _apiKeyService = apiKeyService;
    private const string ApiUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest";

    public async Task<GetCurrencyRateResponse> GetCryptoRatesAsync(string symbol, CancellationToken cancellationToken)
    {
        var result = new GetCurrencyRateResponse { Symbol = symbol };
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
                return new CurrencyRateResult { Convert = convert, Price = null, Error = "Request failed" };
            }

            var data = await response.Content.ReadFromJsonAsync<CryptoRateResponse>(cancellationToken);
            if (data!.Status.ErrorMessage is not null || data.Data is null || data.Data.Count == 0)
            {
                return new CurrencyRateResult { Convert = convert, Price = null, Error = "The provided cryptocurrency symbol is invalid." };
            }

            var price = data.Data[symbol].Quote![convert].Price;
            return new CurrencyRateResult { Convert = convert, Price = price, Error = null };
        });
        
        var responses = await Task.WhenAll(tasks);

        foreach (var response in responses)
        {
            if (response.Error != null)
            {
                result.Error = new ValidationError
                {
                    Code = 400,
                    Message = response.Error
                };
                break;
            }
            result.Prices[response.Convert!] = response.Price;
        }

        return result;
    }
}