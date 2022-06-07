﻿using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.Configuration;
using ChannelEngine.Application.External.Responses;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task<ProductResponse> GetProduct(string productId)
        {
            var url = $"api/v{version}/products/{productId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var product = JsonSerializer.Deserialize<ProductResponse>(jsonResponse);
            ArgumentNullException.ThrowIfNull(product);

            return product;
        }

        public async Task UpdateProductStock(string productId, int value)
        {
            var url = $"api/v{version}/products/{productId}";

            // TODO: change the dto for patching
            var patchDoc = new JsonPatchDocument<ProductResponse>();
            patchDoc.Replace(x => x.Stock, value);
            var serializedDoc = JsonSerializer.Serialize(patchDoc);
            var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync(url, requestContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
