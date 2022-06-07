﻿using System.Text.Json.Serialization;

namespace ChannelEngine.Application.ChannalEngineApi.Responses
{
    public record OrderProductResponse
    {
        public string Status { get; init; } = string.Empty;

        [JsonPropertyName("MerchantProductNo")]
        public string MerchantProductNubmer { get; init; } = string.Empty;
        public int Quantity { get; init; }
    }
}
