using System.Text.Json.Serialization;

namespace ChannelEngine.ExternalApi.Responses
{
    public record OrderResponse
    {
        public string Status { get; init; }

        [JsonPropertyName("Lines")]
        public IReadOnlyList<ProductResponse> Products { get; init; }

    }
}
