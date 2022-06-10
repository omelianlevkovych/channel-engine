using ChannelEngine.Application.Exceptions;

namespace ChannelEngine.Console.Exceptions
{
    public class ProductToPatchIsMissing : ChannelEngineException
    {
        public ProductToPatchIsMissing()
            : base($"Product to patch is missing.")
        { }
    }
}
