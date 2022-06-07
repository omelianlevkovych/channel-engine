using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.External.Responses;

namespace ChannelEngine.Application.ChannalEngineApi.Client.Interfaces
{
    public interface IChannelEngineApiClient
    {
        Task<OrderItemsResponse> GetOrdersByStatus(IEnumerable<OrderStatus> status);
        Task<ProductResponse> GetProduct(string productId);
        Task UpdateProductStock(string productId, int stock);
    }
}
