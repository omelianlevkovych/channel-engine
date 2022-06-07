namespace ChannelEngine.Console.DTOs
{
    internal record OrderDto
    {
        public int Id { get; init; }
        public string? Status { get; init; }
    }
}
    