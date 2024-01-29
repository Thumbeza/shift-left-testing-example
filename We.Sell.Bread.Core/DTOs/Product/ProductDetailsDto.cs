namespace We.Sell.Bread.Core.DTOs.Product
{
    public class ProductDetailsDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }

        public ProductDetailsDto(Guid productId, string productName, decimal price, string description, int stockQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Description = description;
            StockQuantity = stockQuantity;
        }
    }
}
