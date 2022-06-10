using Newtonsoft.Json;

namespace ChannelEngine.Application.External.Responses
{
    public record OrderProductResponse
    {
        [JsonProperty("MerchantProductNo")]
        public string Id { get; init; } = string.Empty;
        public int Quantity { get; init; }

        [JsonProperty("Gtin")]
        public string GlobalTradeItemNumber { get; init; } = string.Empty;
    }
}
