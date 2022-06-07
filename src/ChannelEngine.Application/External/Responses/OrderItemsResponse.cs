using System.Text.Json.Serialization;

namespace ChannelEngine.Application.ChannalEngineApi.Responses
{
    public record OrderItemsResponse
    {
        [JsonPropertyName("Content")]
        public IReadOnlyList<OrderResponse> Orders { get; init; }
        public int TotalCount { get; init; }
    }
}
