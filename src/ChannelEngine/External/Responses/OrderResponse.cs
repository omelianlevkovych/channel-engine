using ChannelEngine.ExternalApi.Responses;
using System.Text.Json.Serialization;

namespace ChannelEngine.External.Responses
{
    public record OrderResponse
    {
        public string Status { get; init; } = string.Empty;

        [JsonPropertyName("Lines")]
        public IReadOnlyList<ProductResponse>? Products { get; init; }

    }
}
