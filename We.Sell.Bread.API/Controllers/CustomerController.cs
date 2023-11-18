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
    private BaseRepository<Customer> _baseRepository;
    private IAsyncOrderRepository _orderRepository;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;

        _baseRepository = new BaseRepository<Customer>(_unitOfWork);
        _orderRepository = new OrderRepository(_unitOfWork);
    }

    [HttpPost(Name = "Post")]
    public async Task<AddCustomer.Response> CreateAsync(AddCustomer.Request model)
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

        await _baseRepository.AddAsync(custormer);
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
