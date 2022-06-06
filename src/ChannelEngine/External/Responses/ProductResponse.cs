﻿using System.Text.Json.Serialization;

namespace ChannelEngine.ExternalApi.Responses
{
    public record ProductResponse
    {
        public string Status { get; init; }

        [JsonPropertyName("MerchantProductNo")]
        public string MerchantProductNubmer { get; init; }
    }
}
