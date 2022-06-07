using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ChannelEngine.Application.ChannalEngineApi.Client
{
    public class ChannelEngineApiClient : IChannelEngineApiClient
    {
        private readonly int version = 2;
        private readonly HttpClient _httpClient;
        private readonly IOrderStatusQueryFactory _orderStatusQueryFactory;
        public ChannelEngineApiClient(
            HttpClient httpClient,
            IChannelEngineApiConfiguration config,
            IOrderStatusQueryFactory orderStatusQueryFactory)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(config.BaseAddress);
            _httpClient.DefaultRequestHeaders.Add(config.ApiKeyHeader, config.ApiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _orderStatusQueryFactory = orderStatusQueryFactory;
        }
        public async Task<OrderItemsResponse> GetOrdersByStatus(IEnumerable<OrderStatus> statuses)
        {
            var url = $"api/v{version}/orders";

            var urlWithQueryParams = _orderStatusQueryFactory.CreateUrl(url, statuses);

            var response = await _httpClient.GetAsync(urlWithQueryParams);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var orders = JsonSerializer.Deserialize<OrderItemsResponse>(jsonResponse);
            ArgumentNullException.ThrowIfNull(orders);

            return orders;
        }
    }
}
