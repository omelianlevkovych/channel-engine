using Newtonsoft.Json;

namespace ChannelEngine.Application.External.Responses
{
    public record OrderResponse
    {
        public int Id { get; init; }
        public string Status { get; init; } = string.Empty;

        [JsonProperty("Lines")]
        public IReadOnlyList<OrderProductResponse>? Products { get; init; }
    }
}
