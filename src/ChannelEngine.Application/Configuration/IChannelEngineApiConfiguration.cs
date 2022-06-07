namespace ChannelEngine.Application.Configuration
{
    public interface IChannelEngineApiConfiguration
    {
        public string BaseAddress { get; }
        public string ApiKeyHeader { get; }
        public string ApiKey { get; }
    }
}
