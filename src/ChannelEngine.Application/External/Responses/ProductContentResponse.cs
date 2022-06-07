using System.Text.Json.Serialization;

namespace ChannelEngine.Application.External.Responses
{
    public record ProductContentResponse
    {
        [JsonPropertyName("MerchantProductNo")]
        public string Id { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public int Stock { get; init; }
    }
}
