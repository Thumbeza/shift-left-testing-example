using We.Sell.Bread.API.Services;
using We.Sell.Bread.Core.DTOs.Product;

namespace We.Sell.Bread.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductController(ILogger<ProductController> logger) : ControllerBase
{
    private readonly ILogger<ProductController> _logger = logger;

    private readonly ProductService _productService = new();

    [HttpGet(Name = "PingProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> HealthCheck()
    {
        return Ok("Product Is ALive");
    }

    [HttpPost(Name = "CreateProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Post(ProductCommand product)
    {
        _logger.LogInformation($"Attempting to create a new product: {product.ProductName}");

        var productDetails = await _productService.AddNewProductAsync(product.ProductName, product.Price, product.Description, product.StockQuantity);

        return productDetails == null ? BadRequest("One or more product details were invalid") : productDetails;
    }

    [HttpGet, Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductDto> Get(string id)
    {

        _logger.LogInformation($"Getting product by Id: {id}");

        var isValid = Guid.TryParse(id, out _);

        if (!isValid)
        {
            return BadRequest($"Product Id: '{id}' is not a valid Guid.");
        }

        var productDetails = _productService.GetProduct(new Guid(id));

        _logger.LogInformation($"A product with Id: '{id}' has been retrieved");

        return productDetails == null ? NotFound($"The product with id: {id} was not found.") : productDetails;
    }

    [HttpGet, Route("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<List<ProductDto>> Get()
    {
        _logger.LogInformation($"Getting all products.");

        var products = _productService.GetAllProducts().ToList();

        _logger.LogInformation("All products have been retrieved");

        return products.Count == 0 ? NoContent() : products;
    }

    [HttpDelete, Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(string id)
    {
        _logger.LogInformation($"Deleting product with ID: {id}");

        var isIdValid = Guid.TryParse(id, out _);

        if (!isIdValid)
        {
            return BadRequest($"Product Id: '{id}' is not a valid Guid.");
        }

        var isProductDeleted = _productService.DeleteProduct(id).Result;

        if (isProductDeleted)
        {
            _logger.LogInformation($"Product {id} deleted");
        }

        return isProductDeleted ? NoContent() : NotFound();
    }
}
