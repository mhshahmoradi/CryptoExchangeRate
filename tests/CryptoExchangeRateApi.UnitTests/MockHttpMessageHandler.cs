using System.Net;

namespace CryptoExchangeRateApi.UnitTests;

public sealed class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly Dictionary<string, decimal> _mockPrices;
    private readonly bool _error;

    public MockHttpMessageHandler(Dictionary<string, decimal> mockPrices, bool error = false)
    {
        _mockPrices = mockPrices;
        _error = error;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_error)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        
        var query = System.Web.HttpUtility.ParseQueryString(request.RequestUri.Query);
        var convert = query["convert"];
        
        if (_mockPrices.ContainsKey(convert))
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($@"{{
                    ""status"": {{}},
                    ""data"": {{
                        ""BTC"": {{
                            ""quote"": {{
                                ""{convert}"": {{
                                    ""price"": {_mockPrices[convert]}
                                }}
                            }}
                        }}
                    }}
                }}")
            };

            return Task.FromResult(response);
        }

        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
    }
}