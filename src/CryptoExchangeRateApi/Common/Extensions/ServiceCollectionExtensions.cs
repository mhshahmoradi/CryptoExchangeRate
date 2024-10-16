namespace CryptoExchangeRateApi.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
        return services;
    }
}