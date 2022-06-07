using ChannelEngine.Application.Exceptions;

namespace ChannelEngine.Application.ChannalEngineApi.Orders.StatusConverter
{
    public class OrderStatusConverter : IOrderStatusConverter
    {
        private readonly Dictionary<OrderStatus, string> _orderStatuses;

        public OrderStatusConverter()
        {
            _orderStatuses = new Dictionary<OrderStatus, string>
            {
                { OrderStatus.InProgress, "IN_PROGRESS" },
                { OrderStatus.InBackorder, "IN_BACKORDER" },
            };
        }
        public string Convert(OrderStatus status)
        {
            var result = _orderStatuses.GetValueOrDefault(status);
            if (string.IsNullOrEmpty(result))
            {
                throw new OrderStatusIsNotSupportedException(status);
            }
            return result;
        }
    }
}
