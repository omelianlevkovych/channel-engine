using ChannelEngine.ExternalApi.Responses;
using System.Text.Json.Serialization;

namespace ChannelEngine.External.Responses
{
    public record OrderResponse
    {
        public string Status { get; init; }

        [JsonPropertyName("Lines")]
        public IReadOnlyList<ProductResponse> Products { get; init; }

    }
}
