using ChannelEngine.Configuration;
using ChannelEngine.ExternalApi.Responses;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChannelEngine.ExternalApi.ApiClient
{
    internal class ChannelEngineApiClient : IChannelEngineApiClient
    {
        private readonly int version = 2;
        private readonly HttpClient _httpClient;
        public ChannelEngineApiClient(HttpClient httpClient, IChannelEngineApiConfiguration config)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(config.BaseAddress);
            _httpClient.DefaultRequestHeaders.Add(config.ApiKeyHeader, config.ApiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<OrderItemsResponse> GetOrdersByStatus(OrderStatus status)
        {
            var requestUri = $"api/v{version}/orders";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var orders = JsonSerializer.Deserialize<OrderItemsResponse>(jsonResponse);
            ArgumentNullException.ThrowIfNull(orders);

            return orders;
        }
    }
}
