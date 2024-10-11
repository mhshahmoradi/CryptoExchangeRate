using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CryptoExchangeRateApi.IntegrationTests.RateLimitTests;

public class RateLimitingTests(WebApplicationFactory<IAssemblyMarker> factory)
    : IClassFixture<WebApplicationFactory<IAssemblyMarker>>
{
    [Fact]
    public async Task CryptoRates_ShouldReturn429_WhenRateLimitExceeded()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        for (int i = 0; i < 5; i++)
        {
            await client.GetAsync("/CryptoRates/api/crypto-rates/BTC");
        }
        
        var rateLimitResponse = await client.GetAsync("/CryptoRates/api/crypto-rates/BTC");

        // Assert
        rateLimitResponse.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
    }
}