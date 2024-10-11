using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CryptoExchangeRateApi.IntegrationTests.CryptoRatesApiTests;

public class CryptoRatesApiTests : IClassFixture<WebApplicationFactory<IAssemblyMarker>>
{
    private readonly HttpClient _client;

    public CryptoRatesApiTests(WebApplicationFactory<IAssemblyMarker> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCryptoRates_ShouldReturn200_WhenSymbolIsValid()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/CryptoRates/api/crypto-rates/BTC");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetCryptoRates_ShouldReturnNull_WhenSymbolIsInvalid()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/CryptoRates/api/crypto-rates/BTRXS");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}