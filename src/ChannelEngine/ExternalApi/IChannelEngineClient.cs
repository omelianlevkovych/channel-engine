namespace ChannelEngine.ExternalApi
{
    internal interface IChannelEngineClient
    {
        Task<IEnumerable<int>> GetOrdersByStatus(OrderStatus status);
    }
}
