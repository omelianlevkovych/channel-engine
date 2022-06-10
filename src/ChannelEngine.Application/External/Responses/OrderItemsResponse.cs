using Newtonsoft.Json;

namespace ChannelEngine.Application.External.Responses
{
    public record OrderItemsResponse
    {
        [JsonProperty("Content")]
        public IReadOnlyList<OrderResponse>? Orders { get; init; }
        public int TotalCount { get; init; }
    }
}
