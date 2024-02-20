using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto>CreateProductAsync(ProductCommand entity);
        ProductDto GetProductById(string productId);
        IEnumerable<ProductDto> GetAllProducts();
        Task<ProductDto> UpdateProduct(ProductDto entity);
    }
}
