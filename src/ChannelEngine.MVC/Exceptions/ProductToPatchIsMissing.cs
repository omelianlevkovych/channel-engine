using ChannelEngine.Application.Exceptions;

namespace ChannelEngine.MVC.Exceptions
{
    public class ProductToPatchIsMissing : ChannelEngineException
    {
        public ProductToPatchIsMissing()
            : base($"Product to patch is missing.")
        { }
    }
}
