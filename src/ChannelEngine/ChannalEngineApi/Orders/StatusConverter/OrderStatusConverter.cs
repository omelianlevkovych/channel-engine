namespace ChannelEngine.ChannalEngineApi.Orders.StatusConverter
{
    public class OrderStatusConverter : IOrderStatusConverter
    {
        private readonly Dictionary<OrderStatus, string> _orderStatuses;

        public OrderStatusConverter()
        {
            _orderStatuses = new Dictionary<OrderStatus, string>
            {
                { OrderStatus.InProgress, "IN_PROGRESS" }
            };
        }
        public string Convert(OrderStatus status)
        {
            var result = _orderStatuses.GetValueOrDefault(status);
            if (string.IsNullOrEmpty(result))
            {
                throw new ApplicationException("Change message");
            }
            return result;
        }
    }
}
