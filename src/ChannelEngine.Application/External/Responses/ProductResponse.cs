namespace ChannelEngine.Application.External.Responses
{
    public record ProductResponse
    {
        public ProductContentResponse Content { get; init; } = new();
    }
}
