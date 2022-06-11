using ChannelEngine.Application.Models;

namespace ChannelEngine.MVC.Models
{
    public record ResponseModel
    {
        public IEnumerable<OrderModel> Orders { get; init; } = new List<OrderModel>();
        public IEnumerable<ProductModel> Products { get; init; } = new List<ProductModel>();
        public ProductViewModel UpdatedProduct { get; init; } = new();
    }
}
