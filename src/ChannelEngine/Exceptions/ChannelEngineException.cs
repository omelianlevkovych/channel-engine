namespace ChannelEngine.Exceptions
{
    public abstract class ChannelEngineException : Exception
    {
        protected ChannelEngineException(string message) : base(message) { }
    }
}
