using ChannelEngine.ExternalApi.Responses;

namespace ChannelEngine.ExternalApi.ApiClient
{
    internal interface IChannelEngineApiClient
    {
        Task<OrderItemsResponse> GetOrdersByStatus(OrderStatus status);
    }
}
