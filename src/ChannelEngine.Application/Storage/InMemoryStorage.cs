using ChannelEngine.Application.Models;
using System.Collections.Concurrent;

namespace ChannelEngine.Application.Gateways
{
    /// <summary>
    /// This class emulates our 'orders in progress' in memory storage.
    /// Our second and third business task requires to work around already retrieved orders.
    /// This is a time tradeoff decision, other option can be some persistence storage (db).
    /// </summary>
    public class InMemoryStorage
    {
        private readonly ConcurrentBag<OrderModel> _ordersInProgress = new();
        public IReadOnlyCollection<OrderModel> OrdersInProgress => _ordersInProgress;

        public void AddOrder(OrderModel order)
        {
            _ordersInProgress.Add(order);
        }

        public void AddOrders(IEnumerable<OrderModel> orders)
        {
            ArgumentNullException.ThrowIfNull(orders);
            foreach (var order in orders)
            {
                AddOrder(order);
            }
        }
    }
}
