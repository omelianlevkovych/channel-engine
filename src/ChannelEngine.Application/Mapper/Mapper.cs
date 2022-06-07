using ChannelEngine.Application.ChannalEngineApi.Responses;
using ChannelEngine.Application.Models;

namespace ChannelEngine.Application.Mapper
{
    public static class Mapper
    {
        public static IEnumerable<OrderModel> ToModel(this IEnumerable<OrderResponse> orders)
        {
            foreach (var order in orders)
            {
                yield return order.ToModel();
            }
        }

        public static OrderModel ToModel(this OrderResponse order)
        {
            var model = new OrderModel
            {
                Id = order.Id,
                Status = order.Status,
            };

            model.AddProducts(order.Products.ToModel());
            return model;
        }

        public static IEnumerable<ProductModel> ToModel(this IEnumerable<OrderProductResponse> products)
        {
            foreach (var product in products)
            {
                yield return product.ToModel();
            }
        }

        public static ProductModel ToModel(this OrderProductResponse product)
        {
            return new ProductModel(product.Id, product.GlobalTradeItemNumber, product.Quantity);
        }
    }
}
