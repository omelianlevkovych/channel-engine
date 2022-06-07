using ChannelEngine.Application.ChannalEngineApi.Responses;
using System.Collections.Concurrent;

namespace ChannelEngine.Application.Gateways
{
    public class OrderGateway
    {
        private ConcurrentBag<OrderResponse> _ordersInProgress = new();
        public IReadOnlyCollection<OrderResponse> OrdersInProgress => _ordersInProgress;

        public void AddOrder(OrderResponse order)
        {
            _ordersInProgress.Add(order);
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
