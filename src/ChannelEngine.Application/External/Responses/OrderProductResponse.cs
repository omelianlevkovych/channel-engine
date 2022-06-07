using System.Text.Json.Serialization;

namespace ChannelEngine.Application.ChannalEngineApi.Responses
{
    public record OrderProductResponse
    {
        [JsonPropertyName("MerchantProductNo")]
        public string Id { get; init; } = string.Empty;
        public int Quantity { get; init; }

        [JsonPropertyName("Gtin")]
        public string GlobalTradeItemNumber { get; init; } = string.Empty;
     }
}
