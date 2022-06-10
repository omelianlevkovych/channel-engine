using ChannelEngine.Application.Models;

namespace ChannelEngine.Application.Storage.Interfaces
{
    public interface IInMemoryStorage
    {
        IReadOnlyCollection<OrderModel> OrdersInProgress { get; }
        void AddOrder(OrderModel order);
        void AddOrders(IEnumerable<OrderModel> orders);
    }
}
