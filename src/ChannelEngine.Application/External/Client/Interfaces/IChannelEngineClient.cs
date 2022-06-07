using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Application.External.Responses;

namespace ChannelEngine.Application.ChannalEngineApi.Client.Interfaces
{
    public interface IChannelEngineApiClient
    {
        Task<OrderItemsResponse> GetOrders(IEnumerable<OrderStatus> filter);
        Task<ProductResponse> GetProduct(string id);
        Task PatchProduct(string id, ProductPatchRequest patch);
    }
}
