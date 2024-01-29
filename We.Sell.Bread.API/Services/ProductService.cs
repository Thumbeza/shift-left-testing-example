﻿using We.Sell.Bread.Core.DTOs.Product;
using We.Sell.Bread.Core.Interfaces;
using We.Sell.Bread.Core.Validations;
using We.Sell.Bread.Infrastructure.Repository;

namespace We.Sell.Bread.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public async Task<ProductDetailsDto> AddNewProductAsync(string productName, decimal price, string description, int stockQuantity)
        {
            Validate.NullOrEmptyArgument(productName);
            Validate.NullOrEmptyArgument(price.ToString());
            Validate.NullOrEmptyArgument(description);
            Validate.NullOrEmptyArgument(stockQuantity.ToString());

            Validate.ArgumentType(productName, typeof(string));

            var prod = new NewProductDto(productName, price, description, stockQuantity);

            var product = await _productRepository.CreateProductAsync(prod);

            return product != null ? product : null;
        }

        public ProductDetailsDto? GetProduct(Guid id)
        {
            Validate.NullOrEmptyArgument(id.ToString());

            var product = _productRepository.GetProductById(id.ToString());

            return product != null ?
                new ProductDetailsDto(id, product.ProductName,product.Price,product.Description,product.StockQuantity) : null;
        }

        public IEnumerable<ProductDetailsDto> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
    }
}
