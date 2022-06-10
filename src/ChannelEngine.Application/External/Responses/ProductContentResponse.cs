using Newtonsoft.Json;

namespace ChannelEngine.Application.External.Responses
{
    public record ProductContentResponse
    {
        [JsonProperty("MerchantProductNo")]
        public string Id { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public int Stock { get; init; }
    }
}
