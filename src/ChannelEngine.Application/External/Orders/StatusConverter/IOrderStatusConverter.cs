using ChannelEngine.Application.External.Orders;

namespace ChannelEngine.Application.External.Orders.StatusConverter
{
    public interface IOrderStatusConverter
    {
        string Convert(OrderStatus status);
    }
}
