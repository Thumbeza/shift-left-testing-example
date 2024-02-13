namespace We.Sell.Bread.Core.DTOs.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }

        public ProductDto(Guid productId, string productName, decimal price, string description, int stockQuantity)
        {
            Id = productId;
            ProductName = productName;
            Price = price;
            Description = description;
            StockQuantity = stockQuantity;
        }
    }
}
