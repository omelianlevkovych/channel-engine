using ChannelEngine.ExternalApi.Responses;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

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
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<OrderItemsResponse> GetOrdersByStatus(OrderStatus status)
        {
            // TODO: resolve header based on status

            var response = await _httpClient.GetAsync("orders");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStreamAsync();

            var orders = JsonSerializer.Deserialize<OrderItemsResponse>(jsonResponse);
            ArgumentNullException.ThrowIfNull(orders);

            return orders;
        }
    }
}
