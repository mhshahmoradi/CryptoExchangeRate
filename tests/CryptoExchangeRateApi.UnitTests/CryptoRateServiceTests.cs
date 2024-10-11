using CryptoExchangeRateApi.Features.CryptoRates.Common.Services.CryptoRateServices;
using CryptoExchangeRateApi.Infrastructure.Services.ApiKeyServices;
using FluentAssertions;
using Moq;

namespace CryptoExchangeRateApi.UnitTests;

public sealed class CryptoRateServiceTests
{
    [Fact]
    public async Task GetCryptoRatesAsync_ShouldReturnPrices_ForMultipleCurrencies()
    {
        // Arrange
        var mockPrices = new Dictionary<string, decimal>
        {
            { "USD", 60000m },
            { "EUR", 50000m },
            { "BRL", 320000m },
            { "GBP", 46000m },
            { "AUD", 90000m }
        };

        var httpClient = new HttpClient(new MockHttpMessageHandler(mockPrices));
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
    
        var apiKeyServiceMock = new Mock<IApiKeyService>();
        apiKeyServiceMock.Setup(x => x.GetNextApiKey()).Returns("mock-api-key");

        var service = new CryptoRateService(httpClientFactoryMock.Object, apiKeyServiceMock.Object);

        // Act
        var result = await service.GetCryptoRatesAsync("BTC", CancellationToken.None);

        // Assert
        result.Prices["USD"].Should().Be(60000m);
        result.Prices["EUR"].Should().Be(50000m);
        result.Prices["BRL"].Should().Be(320000m);
        result.Prices["GBP"].Should().Be(46000m);
        result.Prices["AUD"].Should().Be(90000m);
        result.HasError.Should().BeFalse();
    }

    [Fact]
    public async Task GetCryptoRatesAsync_ShouldReturnError_WhenApiReturnsError()
    {
            // Arrange
            var symbol = "BTC";
            var mockPrices = new Dictionary<string, decimal>
            {
                { "USD", 60000m },
                { "EUR", 50000m },
                { "BRL", 320000m },
                { "GBP", 46000m },
                { "AUD", 90000m }
            };
            
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockPrices, true));
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
    
            var apiKeyServiceMock = new Mock<IApiKeyService>();
            apiKeyServiceMock.Setup(x => x.GetNextApiKey()).Returns("mock-api-key");

            var service = new CryptoRateService(httpClientFactoryMock.Object, apiKeyServiceMock.Object);

            // Act
            var result = await service.GetCryptoRatesAsync(symbol, CancellationToken.None);

            // Assert
            result.Should().NotBeNull(); 
            result.HasError.Should().BeTrue(); 
            result.Error.Should().NotBeNull();
    }
}