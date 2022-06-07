using ChannelEngine.Application.ChannalEngineApi.Responses;
using System.Collections.Concurrent;

namespace ChannelEngine.Application.Gateways
{
    public class OrderGateway
    {
        private ConcurrentBag<OrderResponse> _orders = new();
        public IReadOnlyCollection<OrderResponse> Orders => _orders;

        public void AddOrder(OrderResponse order)
        {
            _orders.Add(order);
        }

        public void AddOrders(IEnumerable<OrderResponse> orders)
        {
            ArgumentNullException.ThrowIfNull(orders);
            foreach (var order in orders)
            {
                AddOrder(order);
            }
        }
    }
}
