using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.External.Responses;
using ChannelEngine.Application.Gateways;

namespace ChannelEngine.Application.BusinessLogic
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly OrderGateway _gateway;
        private readonly IChannelEngineApiClient _channelApi;

        public BusinessLogic(OrderGateway gateway, IChannelEngineApiClient channelApi)
        {
            _gateway = gateway;
            _channelApi = channelApi;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByStatus(IEnumerable<OrderStatus> status)
        {
            var response = await _channelApi.GetOrdersByStatus(status);

            foreach (var order in response.Orders ?? new List<OrderResponse>())
            {
                _gateway.AddOrder(order);
            }

            return _gateway.Orders;
        }

        public Task<ProductResponse> GetProduct(string productId)
        {
            var product = _channelApi.GetProduct(productId);
            return product;
        }

        public IEnumerable<OrderProductResponse> GetTopProductsDesc(int count)
        {
            var orders = _gateway.Orders;
            var products = new List<OrderProductResponse>();

            foreach (var order in orders)
            {
                products.AddRange(order.Products ?? new List<OrderProductResponse>());
            }

            return products.OrderByDescending(x => x.Quantity).Take(count);
        }

        public Task UpdateProductStock(string productId, int value)
        {
            return _channelApi.UpdateProductStock(productId, value);
        }
    }
}
