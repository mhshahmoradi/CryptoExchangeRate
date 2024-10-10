namespace CryptoExchangeRateApi.Common.Extensions;

public static class HttpClientExtensions
{
    public static void AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient("CryptoClient", client =>
        {
            client.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/v1/cryptocurrency");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
    }
}