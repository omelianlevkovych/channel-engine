namespace ChannelEngine.Application.Models
{
    public record ProductViewModel
    {
        public string Id { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public int Stock { get; init; }
    }
}
