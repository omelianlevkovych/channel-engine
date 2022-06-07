using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.External.Responses;

namespace ChannelEngine.Application.BusinessLogic
{
    public interface IBusinessLogic
    {
        Task<IEnumerable<OrderResponse>> GetOrdersByStatus(IEnumerable<OrderStatus> status);
        //TODO: make it more flexible by using Func<T> for filtering.
        IEnumerable<OrderProductResponse> GetTopProductsDesc(int count);
        Task<ProductResponse> GetProduct(string productId);
        Task UpdateProductStock(string productId, int value);
    }
}
