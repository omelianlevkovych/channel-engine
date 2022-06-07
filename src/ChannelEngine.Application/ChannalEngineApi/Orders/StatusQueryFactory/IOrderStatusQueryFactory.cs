namespace ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory
{
    public interface IOrderStatusQueryFactory
    {
        string CreateUrl(string url, IEnumerable<OrderStatus> statuses);
    }
}
