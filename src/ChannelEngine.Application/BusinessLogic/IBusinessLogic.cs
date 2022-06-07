using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Application.External.Responses;
using ChannelEngine.Application.Models;

namespace ChannelEngine.Application.BusinessLogic
{
    public interface IBusinessLogic
    {
        Task<IEnumerable<OrderModel>> GetOrders(IEnumerable<OrderStatus> status);
        Task<ProductResponse> GetProduct(string productId);
        Task<IEnumerable<ProductModel>> GetTopProductsDesc(int count);
        Task PatchProduct(string productId, ProductPatchRequest patch);
    }
}
