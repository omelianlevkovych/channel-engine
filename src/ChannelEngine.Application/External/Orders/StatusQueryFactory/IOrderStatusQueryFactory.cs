using ChannelEngine.Application.External.Orders;

namespace ChannelEngine.Application.External.Orders.StatusQueryFactory
{
    public interface IOrderStatusQueryFactory
    {
        string CreateUrl(string url, IEnumerable<OrderStatus> statuses);
    }
}
