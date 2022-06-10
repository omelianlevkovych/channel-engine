namespace ChannelEngine.Console.DTOs
{
    internal sealed record OrderDto
    {
        public int Id { get; init; }
        public string? Status { get; init; }
    }
}
