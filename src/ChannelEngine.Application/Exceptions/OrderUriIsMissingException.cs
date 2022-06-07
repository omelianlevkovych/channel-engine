namespace ChannelEngine.Application.Exceptions
{
    public class OrderUriIsMissingException : ChannelEngineException
    {
        public OrderUriIsMissingException()
            : base("Uri is missing exception.") { }
    }
}
