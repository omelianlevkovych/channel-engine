using ChannelEngine.External.Responses;
using System.Text.Json.Serialization;

namespace ChannelEngine.ExternalApi.Responses
{
    public record OrderItemsResponse
    {
        [JsonPropertyName("Content")]
        public IReadOnlyList<OrderResponse>? Orders { get; init; }
        public int TotalCount { get; init; }
    }
}
