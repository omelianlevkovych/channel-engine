using ChannelEngine.Application.ChannalEngineApi.Orders.StatusConverter;
using ChannelEngine.Application.Exceptions;
using Microsoft.AspNetCore.WebUtilities;

namespace ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory
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
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new OrderUriIsMissingException();
            }
            ArgumentNullException.ThrowIfNull(statuses);

            var distinctStatuses = statuses.Distinct().ToList();

            var queryParams = new List<KeyValuePair<string, string>>();

            foreach (var status in distinctStatuses)
            {
                var statusAsString = _statusConverter.Convert(status);
                queryParams.Add(new KeyValuePair<string, string>(_statusesQueryName, statusAsString));
            }

            string result = url;

            foreach (var param in queryParams)
            {
                result = QueryHelpers.AddQueryString(result, param.Key, param.Value);
            }

            return result;
        }
    }
}
