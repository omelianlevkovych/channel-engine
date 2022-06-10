namespace ChannelEngine.Application.Exceptions
{
    public class ProductIsNotFoundException : ChannelEngineException
    {
        public string ProductId { get; }

        public ProductIsNotFoundException(string productId)
            : base($"Product '{productId}' is not found.") => ProductId = productId;
    }
}
