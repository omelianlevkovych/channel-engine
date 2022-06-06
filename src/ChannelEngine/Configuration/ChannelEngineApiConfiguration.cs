using Microsoft.Extensions.Configuration;

namespace ChannelEngine.Configuration
{
    public class ChannelEngineApiConfiguration : IChannelEngineApiConfiguration
    {
        private const string SECTION = "ChannelEngineApi";
        private readonly IConfiguration _config;
        public ChannelEngineApiConfiguration(IConfiguration config)
        {
            this._config = config;
        }

        public string BaseAddress => _config.GetValue($"{SECTION}:BaseAddress", string.Empty);

        public string ApiKey => _config.GetValue($"{SECTION}:ApiKey", string.Empty);

        public string ApiKeyHeader => _config.GetValue($"{SECTION}:ApiKeyHeader", string.Empty);
    }
}
