namespace ChannelEngine.Application.BusinessLogic
{
    public interface IBusinessLogic
    {
        Task<int> GetOrdersByStatus(string status);
    }
}
