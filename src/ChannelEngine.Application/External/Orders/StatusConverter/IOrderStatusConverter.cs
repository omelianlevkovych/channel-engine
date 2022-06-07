namespace ChannelEngine.Application.ChannalEngineApi.Orders.StatusConverter
{
    public interface IOrderStatusConverter
    {
        string Convert(OrderStatus status);
    }
}
