using ChannelEngine.Application.Exceptions;
using ChannelEngine.Application.External.Client.Interfaces;
using ChannelEngine.Application.External.Orders;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Application.Mapper;
using ChannelEngine.Application.Models;
using ChannelEngine.Application.Storage.Interfaces;
using Microsoft.Extensions.Logging;

namespace ChannelEngine.Application.BusinessLogics
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IInMemoryStorage _storage;
        private readonly IChannelEngineApiClient _channelEngineApiClient;
        private readonly ILogger<BusinessLogic> _logger;

        public BusinessLogic(
            IInMemoryStorage storage,
            IChannelEngineApiClient channelEngineApiClient,
            ILogger<BusinessLogic> logger)
        {
            _storage = storage;
            _channelEngineApiClient = channelEngineApiClient;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderModel>> GetOrders(IEnumerable<OrderStatus> filter)
        {
            _logger.LogInformation("Started getting orders");

            var response = await _channelEngineApiClient.GetOrders(filter);

            if (response.Orders is null)
            {
                throw new OrdersAreMissingException();
            }

            foreach (var order in response.Orders)
            {
                _storage.AddOrder(order.ToModel());
            }

            return _storage.OrdersInProgress;
        }

        public async Task<ProductViewModel> GetProduct(string id)
        {
            _logger.LogInformation("Started getting product '{id}'.", id);

            var product = await _channelEngineApiClient.GetProduct(id);
            if (product is null)
            {
                throw new ProductIsNotFoundException(id);
            }
            return product.ToViewModel();
        }

        public async Task<IReadOnlyCollection<ProductModel>> GetTopProductsDesc(int count)
        {
            _logger.LogInformation("Started getting '{count}' products in descending order.", count);

            var orders = _storage.OrdersInProgress;

            var products = await GetCompleteProducts();

            var result = products
                .OrderByDescending(x => x.TotalQuantity)
                .Take(count);


            return result.ToList().AsReadOnly();
        }

        public Task PatchProduct(string id, ProductPatchRequest patch)
        {
            _logger.LogInformation("Started patching product '{id}'", id);

            return _channelEngineApiClient.PatchProduct(id, patch);
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
                var response = await _channelEngineApiClient.GetProduct(product.Id);
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
