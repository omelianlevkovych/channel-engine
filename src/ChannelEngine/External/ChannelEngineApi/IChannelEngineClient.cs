namespace ChannelEngine.ExternalApi.ApiClient
{
    internal interface IChannelEngineApiClient
    {
        Task<string> GetOrdersByStatus(OrderStatus status);
    }
}
