
1. How long did you spend on the coding assignment? What would you add to
your solution if you had more time? If you didn't spend much time on the coding
assignment then use this as an opportunity to explain what you would add.

Answer: I spent about one day on the coding assignment. I was somewhat focused on how I could improve the performance, and two ideas came to mind:

-   The first one was using a **round-robin algorithm** for selecting the API keys, so I could distribute requests across multiple keys and avoid hitting the rate limit.
-   The second one was due to the fact that our API key was a free version, and it had limitations on retrieving all currency prices in one API call. The service provider imposed this limitation, so I sent 5 simultaneous API calls to reduce the waiting time.
- The third one was using rate limit for api.
- The last one is using minimal api and vertical slice to keep project simple and fast.

If I had more time, I would have liked to integrate **Polly** for implementing a **retry mechanism**. Additionally, I would have implemented the **circuit breaker pattern** to route requests to a secondary service when the primary service encounters issues. I would also have added a feature that allows users to set their desired price alerts, and once the currency reaches that price, notifications would be sent to them via various methods like email.

2. What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.

Answer: **Definitely the Primary Constructor feature.**  
I absolutely love this feature because it helps make the code more concise and cleaner. I really appreciate the direction the .NET team is taking by simplifying code and reducing complexity.

For example, a typical constructor-based code like this:

    public sealed class CryptoRateService: ICryptoRateService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiKeyService _apiKeyService;
    
        public CryptoRateService(IHttpClientFactory httpClientFactory, IApiKeyService apiKeyService)
        {
            _httpClient = httpClientFactory.CreateClient("CryptoClient");
            _apiKeyService = apiKeyService;
        }
    }

Can be simplified into:

    public sealed class CryptoRateService(IHttpClientFactory httpClientFactory, IApiKeyService apiKeyService)
        : ICryptoRateService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
        private readonly IApiKeyService _apiKeyService = apiKeyService;
    }

You can see how much simpler and cleaner it is :)

3. How would you track down a performance issue in production? Have you ever had to do this?

Answer: I have had a similar experience where the response time of our APIs had increased, and I was responsible for investigating the issue. Ultimately, I was able to improve performance by three to four times through proper indexing on the database, analyzing queries, rewriting them, and utilizing compiled queries for heavy queries.

4. What was the latest technical book you have read or tech conference you have been to? What did you learn?

Answer: The latest book I read is Concurrency in C# Cookbook. I am still in the process of reading it, but I have gained a solid understanding of how asynchronous programming and parallel programming work. I also became familiar with state machines and thread pools, which were very interesting, and I was able to better comprehend some of the code I write.

Additionally, I started reading another book called Unit Testing Principles, Practices, and Patterns, where I learned about different styles of writing tests, such as classic and London styles. Overall, I gained insight into how to write good tests.

The last conference I attended was hosted by Ibrahim Nabiei, which focused on agile methodologies and technical debt. It really gave me a better perspective for my future and was one of the most beneficial conferences I've attended.

5. What do you think about this technical assessment?

Answer: I think the technical assessment was great, and I really enjoyed it. It allowed me to become familiar with APIs in the cryptocurrency domain, and ultimately, it was very beneficial for me.

6. Please, describe yourself using JSON.

>     {
>          "name": "MohammadReza Shahmorady",
>          "age": 19,
>          "profession": "Software Engineer",
>          "interests": [
>            "open source",
>            "Linux",
>            "Dotnet"
>          ],
>          "background": {
>            "startedProgrammingAt": 16,
>            "initialFocus": "game development",
>            "currentFocus": "backend development"
>          },
>          "personalTraits": [
>            "eager to learn challenging things",
>            "obsessive about performance"
>          ]
>     }
