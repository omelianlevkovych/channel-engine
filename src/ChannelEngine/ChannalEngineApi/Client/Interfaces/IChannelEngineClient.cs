using ChannelEngine.ChannalEngineApi.Orders;
using ChannelEngine.ExternalApi.Responses;

namespace ChannelEngine.ChannalEngineApi.Client.Interfaces
{
    internal interface IChannelEngineApiClient
    {
        Task<OrderItemsResponse> GetOrdersByStatus(IEnumerable<OrderStatus> status);
    }
}
