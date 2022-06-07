using ChannelEngine.Application.ChannalEngineApi.Client.Interfaces;
using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.Gateways;

namespace ChannelEngine.Application.BusinessLogic
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly OrderGateway _ordersGateway;
        private readonly IChannelEngineApiClient _channelApi;

        public BusinessLogic(OrderGateway ordersGateway, IChannelEngineApiClient channelApi)
        {
            _ordersGateway = ordersGateway;
            _channelApi = channelApi;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByStatus(IEnumerable<OrderStatus> status)
        {
            // call 3rd party
            // set ordergateway
            // return result from ordergateway
            var response = await _channelApi.GetOrdersByStatus(status);
            
            // in memory persistance
            foreach (var order in response.Orders ?? new List<OrderResponse>())
            {
                _ordersGateway.AddOrder(order);
            }

            return _ordersGateway.Orders;
        }

        public IEnumerable<ProductResponse> GetTopProductsDesc(int count)
        {
            var orders = _ordersGateway.Orders;
            var products = new List<ProductResponse>();

            foreach (var order in orders)
            {
                products.AddRange(order.Products ?? new List<ProductResponse>());
            }

            return products.OrderByDescending(x => x.Quantity).Take(count);
        }
    }
}
