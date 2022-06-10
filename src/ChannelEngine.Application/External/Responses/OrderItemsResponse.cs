using Newtonsoft.Json;

namespace ChannelEngine.Application.ChannalEngineApi.Responses
{
    public record OrderItemsResponse
    {
        [JsonProperty("Content")]
        public IReadOnlyList<OrderResponse> Orders { get; init; }
        public int TotalCount { get; init; }
    }
}
