using We.Sell.Bread.API.DTOs.Customers;
using We.Sell.Bread.Infrastructure.Data;
using We.Sell.Bread.Infrastructure.Data.Repositories;

namespace We.Sell.Bread.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CustomerController : ControllerBase
{
    private readonly IUnitOfWork<OrderDBContext> _unitOfWork = new UnitOfWork<OrderDBContext>();

    private readonly ILogger<CustomerController> _logger;
    private GenericRepository<Customer> _baseRepository;
    private IOrderRepository _orderRepository;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;

        _baseRepository = new GenericRepository<Customer>(_unitOfWork);
        _orderRepository = new OrderRepository(_unitOfWork);
    }

    [HttpPost(Name = "Post")]
    public async Task<AddCustomer.Response> PostAsync(AddCustomer.Request model)
    {

        _logger.LogInformation($"Attempting to create a neew customer: {model.CustomerName}");

        var custormer = new Customer
        {
            Id = Guid.NewGuid(),
            CustomerName = model.CustomerName,
            ContactNo = model.ContactNo,
            EmailAddress = model.EmailAddress,
            PhysicalAddress = model.PhysicalAddress
        };

        _baseRepository.Add(custormer);
        await _unitOfWork.SaveChangesAsync();

        var response = new AddCustomer.Response
        {
            Id = custormer.Id,
            CustomerName = custormer.CustomerName
        };

        _logger.LogInformation($"New custormer hase been created: {response.Id}");

        return response;
    }
}
