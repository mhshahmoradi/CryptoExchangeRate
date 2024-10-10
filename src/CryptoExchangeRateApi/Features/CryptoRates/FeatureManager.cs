using ServiceCollector.Abstractions;

namespace CryptoExchangeRateApi.Features.CryptoRates;

public sealed class FeatureManager
{
    public const string EndpointTagName = "CryptoRate";
    public const string Prefix = "/CryptoRates";

    public sealed class ServiceManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            
        }
    }
}