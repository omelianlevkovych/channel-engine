using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.Configuration;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Application.External.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ChannelEngine.Application.ChannalEngineApi.Client
{
    public sealed class ChannelEngineApiClient : IChannelEngineApiClient
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
            _orderStatusQueryFactory = orderStatusQueryFactory;

            _httpClient.BaseAddress = new Uri(config.BaseAddress);
            _httpClient.DefaultRequestHeaders.Add(config.ApiKeyHeader, config.ApiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<OrderItemsResponse> GetOrders(IEnumerable<OrderStatus> filter)
        {
            var url = $"api/v{version}/orders";
            var urlWithQueryParams = _orderStatusQueryFactory.CreateUrl(url, filter);

            var request = new HttpRequestMessage(HttpMethod.Get, urlWithQueryParams);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<OrderItemsResponse>(responseJson);
            ArgumentNullException.ThrowIfNull(orders);

            return orders;
        }

        public async Task<ProductResponse> GetProduct(string productId)
        {
            var url = $"api/v{version}/products/{productId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductResponse>(jsonResponse);
            ArgumentNullException.ThrowIfNull(product);

            return product;
        }

        public async Task PatchProduct(string productId, ProductPatchRequest patch)
        {
            var url = $"api/v{version}/products/{productId}";

            var patchObject = new
            {
                Op = "replace",
                Value = patch.Stock,
                Path = nameof(patch.Stock),
            };

            // For some reason, PATCH /v2/products only validates array but does not work with json object.
            var array = new[] { patchObject };

            var serializedArray = JsonConvert.SerializeObject(array);
            var requestContent = new StringContent(serializedArray, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync(url, requestContent);
            response.EnsureSuccessStatusCode();
        }

        // This does not work, meanwhile it returns 200 with AcceptedCount not zero.
        // It would be nice if you can explain what was the problem here.
        [Obsolete]
        private async Task PatchProductLegacy(string productId, ProductPatchRequest patch)
        {
            var url = $"api/v{version}/products/{productId}";

            var patchDoc = new JsonPatchDocument<ProductPatchRequest>();
            patchDoc.Replace(x => x.Stock, patch.Stock);

            var serializedDoc = JsonConvert.SerializeObject(patchDoc);
            var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync(url, requestContent);
            var responseJson = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }
    }
}
