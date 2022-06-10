namespace ChannelEngine.Application.Exceptions
{
    public class OrdersAreMissingException : ChannelEngineException
    {
        public OrdersAreMissingException() 
            : base("Sorry, orders are missing!")
        { }
    }
}
