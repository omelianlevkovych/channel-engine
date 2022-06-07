namespace ChannelEngine.Application.Models
{
    public class ProductModel
    {
        public string Id { get; init; }
        public string GlobalTradeItemNumber { get; init; }
        private int _totalQuantity;
        public int TotalQuantity { get { return _totalQuantity; } }
        private string _name;
        public string Name { get { return _name; } }

        public ProductModel(string id, string GTN, int totalQuantity)
        {
            Id = id;
            GlobalTradeItemNumber = GTN;
            _totalQuantity = totalQuantity;
            _name = string.Empty;
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
