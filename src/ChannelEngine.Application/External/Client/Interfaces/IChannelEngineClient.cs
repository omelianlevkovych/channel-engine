using ChannelEngine.Application.External.Orders;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Application.External.Responses;

namespace ChannelEngine.Application.External.Client.Interfaces
{
    public interface IChannelEngineApiClient
    {
        Task<OrderItemsResponse> GetOrders(IEnumerable<OrderStatus> filter);
        Task<ProductResponse> GetProduct(string id);
        Task PatchProduct(string id, ProductPatchRequest patch);
    }
}
