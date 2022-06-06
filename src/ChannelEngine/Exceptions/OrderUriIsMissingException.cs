namespace ChannelEngine.Exceptions
{
    internal class OrderUriIsMissingException : ChannelEngineException
    {
        public OrderUriIsMissingException()
            : base("Uri is missing exception.") { }
    }
}
