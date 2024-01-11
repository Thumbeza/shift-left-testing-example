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
    private ICustomerRepository _customerRepository;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;

        _baseRepository = new GenericRepository<Customer>(_unitOfWork);
        _customerRepository = new CustomerRepository(_unitOfWork);
    }

    [HttpPost(Name = "Post")]
    public CustomerDetailsDto Post(NewCustomerDto customer)
    {

        _logger.LogInformation($"Attempting to create a neew customer: {customer.CustomerName}");

        if (customer == null) 
        {
            throw new ArgumentNullException("Empty Customer", "Customer details must be added before adding a new customer.");
        }

        var newCustormer = new Customer
        {
            Id = Guid.NewGuid(),
            CustomerName = customer.CustomerName,
            ContactNo = customer.ContactNo,
            EmailAddress = customer.EmailAddress,
            PhysicalAddress = customer.PhysicalAddress
        };

        _baseRepository.Create(newCustormer);
        //await _unitOfWork.SaveChangesAsync();

        var response = new CustomerDetailsDto
        {
            Id = newCustormer.Id,
            CustomerName = newCustormer.CustomerName,
            ContactNo = newCustormer.ContactNo,
            EmailAddress = newCustormer.EmailAddress,
            PhysicalAddress = newCustormer.PhysicalAddress
        };

        _logger.LogInformation($"New custormer hase been created: {response.Id}");

        return response;
    }

    [HttpGet(Name = "GetById")]
    public CustomerDetailsDto GetById(string customerId)
    {

        _logger.LogInformation($"Getting custormer by Id: {customerId}");

        if (string.IsNullOrEmpty(customerId)) 
        {
            throw new ArgumentNullException(customerId, "Customer Id cannot not be null.");
        }

        var custormer = _customerRepository.GetCustomerById(Guid.Parse(customerId));

        var response = new CustomerDetailsDto
        {
            Id = custormer.Id,
            CustomerName = custormer.CustomerName,
            ContactNo = custormer.ContactNo,
            EmailAddress = custormer.EmailAddress,
            PhysicalAddress = custormer.PhysicalAddress
        };

        _logger.LogInformation($"A cusstomer with Id: {customerId} has been retrieved");

        return response;
    }
}
