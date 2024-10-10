namespace CryptoExchangeRateApi.Infrastructure.Services.ApiKeyServices;

public sealed class ApiKeyService : IApiKeyService
{
    private readonly List<string> _apiKeys;
    private int _currentIndex = -1;

    public ApiKeyService(IConfiguration configuration)
    {
        _apiKeys = configuration.GetSection("ApiKeys").Get<List<string>>()!;
    }

    public string GetNextApiKey()
    {
        return _apiKeys[Interlocked.Increment(ref _currentIndex) % _apiKeys.Count];
    }
}