namespace ChannelEngine.Application.Models
{
    public class OrderModel
    {
        public int Id { get; init; }
        public string Status { get; init; } = string.Empty;

        private readonly List<ProductModel> _products = new();
        public IReadOnlyList<ProductModel> Products => _products;

        public void AddProducts(IEnumerable<ProductModel> products)
        {
            ArgumentNullException.ThrowIfNull(products);
            _products.AddRange(products);
        }
    }
}
