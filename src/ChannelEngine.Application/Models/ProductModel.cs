namespace ChannelEngine.Application.Models
{
    public class ProductModel
    {
        public string Id { get; init; }
        public string GlobalTradeItemNumber { get; init; }

        private int _totalQuantity;
        public int TotalQuantity => _totalQuantity;

        private string? _name;
        public string Name => string.IsNullOrWhiteSpace(_name) ? string.Empty : _name;

        public ProductModel(string id, string GTN, int totalQuantity)
        {
            Id = id;
            GlobalTradeItemNumber = GTN;
            _totalQuantity = totalQuantity;
        }

        public void IncrementQuantity(int value)
        {
            _totalQuantity += value;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            _name = name;
        }
    }
}
