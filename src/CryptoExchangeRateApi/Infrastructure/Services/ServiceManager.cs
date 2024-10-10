using CryptoExchangeRateApi.Infrastructure.Services.ApiKeyServices;
using ServiceCollector.Abstractions;

namespace CryptoExchangeRateApi.Infrastructure.Services;

public sealed class ServiceManager : IServiceDiscovery
{
    public void AddServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IApiKeyService, ApiKeyService>();
    }
}