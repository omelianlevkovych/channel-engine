using ChannelEngine.ChannalEngineApi.Orders.StatusConverter;
using Microsoft.AspNetCore.WebUtilities;

namespace ChannelEngine.ChannalEngineApi.Orders.StatusQueryFactory
{
    public class OrderStatusQueryFactory : IOrderStatusQueryFactory
    {
        private readonly string _statusesQueryName;
        private readonly IOrderStatusConverter _statusConverter;

        public OrderStatusQueryFactory(IOrderStatusConverter statusConverter)
        {
            _statusConverter = statusConverter;
            _statusesQueryName = "statuses".ToLower();
        }

        public string CreateUrl(string url, IEnumerable<OrderStatus> statuses)
        {
            var distinctStatuses = statuses.Distinct().ToList();

            var queryParams = new Dictionary<string, string>();

            foreach (var status in distinctStatuses)
            {
                var statusAsString = _statusConverter.Convert(status);
                queryParams.Add(_statusesQueryName, statusAsString);
            }

            var result = QueryHelpers.AddQueryString(url, queryParams);
            return result;
        }
    }
}
