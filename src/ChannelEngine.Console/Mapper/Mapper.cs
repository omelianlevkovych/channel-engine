using ChannelEngine.Application.Models;
using ChannelEngine.Console.DTOs;

namespace ChannelEngine.Console.Mapper
{
    internal static class Mapper
    {
        public static OrderDto ToDto(this OrderModel model)
        {
            return new OrderDto
            {
                Id = model.Id,
                Status = model.Status,
            };
        }

        public static IEnumerable<OrderDto> ToDto(this IEnumerable<OrderModel> models)
        {
            foreach (var model in models)
            {
                yield return model.ToDto();
            }
        }
    }
}
