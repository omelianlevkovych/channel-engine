using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Responses;

namespace ChannelEngine.Application.BusinessLogic
{
    public interface IBusinessLogic
    {
        Task<IEnumerable<OrderResponse>> GetOrdersByStatus(IEnumerable<OrderStatus> status);
        IEnumerable<ProductResponse> GetTopProductsDesc(int count);
    }
}
