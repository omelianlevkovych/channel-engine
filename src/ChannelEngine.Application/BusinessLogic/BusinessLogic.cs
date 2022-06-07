﻿using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Application.Gateways;
using ChannelEngine.Application.Mapper;
using ChannelEngine.Application.Models;

namespace ChannelEngine.Application.BusinessLogic
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly InMemoryStorage _storage;
        private readonly IChannelEngineApiClient _channelApi;

        public BusinessLogic(InMemoryStorage storage, IChannelEngineApiClient channelApi)
        {
            _storage = storage;
            _channelApi = channelApi;
        }

        public async Task<IEnumerable<OrderModel>> GetOrders(IEnumerable<OrderStatus> filter)
        {
            var response = await _channelApi.GetOrders(filter);

            foreach (var order in response.Orders)
            {
                _storage.AddOrder(order.ToModel());
            }

            return _storage.OrdersInProgress;
        }

        public async Task<ProductViewModel> GetProduct(string id)
        {
            var product = await _channelApi.GetProduct(id);
            if (product is null)
            {
                throw new Exception("custom product not found exception");
            }
            return product.ToViewModel();
        }

        public async Task<IEnumerable<ProductModel>> GetTopProductsDesc(int count)
        {
            var orders = _storage.OrdersInProgress;

            var products = await GetCompleteProducts();

            return products.OrderByDescending(x => x.TotalQuantity).Take(count);
        }

        public Task PatchProduct(string id, ProductPatchRequest patch)
        {
            return _channelApi.PatchProduct(id, patch);
        }

        /// <summary>
        /// Unfortunatelly we do not have all fields provided in /orders endpoint.
        /// Therefore we are calling /products endpoint for each product from orders list.
        /// </summary>
        /// <returns>Complete product.</returns>
        private async Task<IEnumerable<ProductModel>> GetCompleteProducts()
        {
            var orders = _storage.OrdersInProgress;
            var products = MergeProductsInOrders(orders);

            // We have to setup names here as /orders do not have this field.
            foreach (var product in products)
            {
                var response = await _channelApi.GetProduct(product.Id);
                product.SetName(response.Content.Name);
            }

            return products;
        }

        /// <summary>
        /// This method goes threw every order and merge it products into unique collection.
        /// Merge is based on product Id (MerchantProductNo); in case we have two same products we are incrementing
        /// it total quantity.
        /// </summary>
        /// <param name="orders">The collection of orders.</param>
        /// <returns>Returns collection of unique products.</returns>
        private static IEnumerable<ProductModel> MergeProductsInOrders(IEnumerable<OrderModel> orders)
        {
            var products = new Dictionary<string, ProductModel>();

            foreach (var order in orders)
            {
                foreach (var product in order.Products)
                {
                    if (products.TryGetValue(product.Id, out var value))
                    {
                        value.IncrementQuantity(product.TotalQuantity);
                    }
                    else
                    {
                        products.Add(product.Id, new ProductModel(
                            product.Id,
                            product.GlobalTradeItemNumber,
                            product.TotalQuantity));
                    }
                }
            }

            return products.Values;
        }
    }
}
