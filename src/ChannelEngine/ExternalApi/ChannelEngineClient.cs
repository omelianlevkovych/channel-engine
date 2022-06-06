namespace ChannelEngine.ExternalApi
{
    internal class ChannelEngineClient : IChannelEngineClient
    {
        private readonly HttpClient _httpClient;
        public ChannelEngineClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // TODO: make version, base url configuratble
            _httpClient.BaseAddress = new Uri("https://api-dev.channelengine.net/api/v2/");
            _httpClient.DefaultRequestHeaders.Add("X-CE-KEY", "key");

        }

        public async Task<IEnumerable<int>> GetOrdersByStatus(OrderStatus status)
        {
            // TODO: resolve header based on status

            var response = await _httpClient.GetAsync("orders");
            response.EnsureSuccessStatusCode();

            // deserialize into typed object
            return null;
        }
    }
}
