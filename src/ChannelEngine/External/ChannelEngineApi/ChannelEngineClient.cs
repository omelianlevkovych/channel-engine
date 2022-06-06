using Microsoft.Extensions.Configuration;

namespace ChannelEngine.ExternalApi.ApiClient
{
    internal class ChannelEngineApiClient : IChannelEngineApiClient
    {
        private readonly HttpClient _httpClient;
        public ChannelEngineApiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;

            var baseUrl = config["ChannelEngineApi:BaseAddress"];
            _httpClient.BaseAddress = new Uri(baseUrl);

            var apiKey = config["ChannelEngineApi:ApiKey"];
            _httpClient.DefaultRequestHeaders.Add("X-CE-KEY", apiKey);
        }
        public async Task<string> GetOrdersByStatus(OrderStatus status)
        {
            // TODO: resolve header based on status

            var response = await _httpClient.GetAsync("orders");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
