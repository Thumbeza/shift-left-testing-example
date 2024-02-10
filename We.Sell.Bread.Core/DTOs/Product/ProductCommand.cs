namespace We.Sell.Bread.Core.DTOs.Product
{
    public class ProductCommand
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }

        public ProductCommand(string productName, decimal price, string description, int stockQuantity)
        {
            ProductName = productName;
            Price = price;
            Description = description;
            StockQuantity = stockQuantity;
        }
    }
}
