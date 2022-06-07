using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Responses;

namespace ChannelEngine.Application.ChannalEngineApi.Client.Interfaces
{
    public interface IChannelEngineApiClient
    {
        Task<OrderItemsResponse> GetOrdersByStatus(IEnumerable<OrderStatus> status);
    }
}
