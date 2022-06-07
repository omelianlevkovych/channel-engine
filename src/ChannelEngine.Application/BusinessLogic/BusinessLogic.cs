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

        public async Task<IEnumerable<OrderModel>> GetOrders(IEnumerable<OrderStatus> status)
        {
            var response = await _channelApi.GetOrdersByStatus(status);

            foreach (var order in response.Orders)
            {
                _storage.AddOrder(order.ToModel());
            }

            return _storage.OrdersInProgress;
        }

        public async Task<ProductViewModel> GetProduct(string productId)
        {
            var product = await _channelApi.GetProduct(productId);
            if (product is null)
            {
                throw new Exception("custom product not found exception");
            }
            return product.ToViewModel();
        }

        public async Task<IEnumerable<ProductModel>> GetTopProductsDesc(int count)
        {
            var orders = _storage.OrdersInProgress;

            var products = await GetProducts();

            return products.OrderByDescending(x => x.TotalQuantity).Take(count);
        }

        public Task PatchProduct(string productId, ProductPatchRequest patch)
        {
            return _channelApi.PatchProduct(productId, patch);
        }

        private async Task<IEnumerable<ProductModel>> GetProducts()
        {
            var orders = _storage.OrdersInProgress;
            var products = MergeProductsInOrders(orders);

            // We have to setup names as well here, the /orders endpoints unfortunatelly does not contain such data.
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
        private IEnumerable<ProductModel> MergeProductsInOrders(IEnumerable<OrderModel> orders)
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
                        products.Add(product.Id, new ProductModel(product.Id, product.GlobalTradeItemNumber, product.TotalQuantity));
                    }
                }
            }

            return products.Values;
        }
    }
}
