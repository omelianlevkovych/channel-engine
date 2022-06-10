using ChannelEngine.Application.External.Orders;
using ChannelEngine.Application.External.Requests;
using ChannelEngine.Application.Models;

namespace ChannelEngine.Application.BusinessLogics
{
    public interface IBusinessLogic
    {
        Task<IEnumerable<OrderModel>> GetOrders(IEnumerable<OrderStatus> filter);
        Task<IEnumerable<ProductModel>> GetTopProductsDesc(int count);
        Task PatchProduct(string id, ProductPatchRequest patch);
        Task<ProductViewModel> GetProduct(string id);
    }
}
