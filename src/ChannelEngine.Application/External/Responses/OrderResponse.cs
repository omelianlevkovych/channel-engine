using System.Text.Json.Serialization;

namespace ChannelEngine.Application.ChannalEngineApi.Responses
{
    public record OrderResponse
    {
        public int Id { get; init; }
        public string Status { get; init; } = string.Empty;

        [JsonPropertyName("Lines")]
        public IReadOnlyList<OrderProductResponse> Products { get; init; }
    }
}
