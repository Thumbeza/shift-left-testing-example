using We.Sell.Bread.Core.Interfaces;
using We.Sell.Bread.Core.DTOs.Customer;
using We.Sell.Bread.Core.Validations;
using We.Sell.Bread.Infrastructure.Repository;

namespace We.Sell.Bread.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService() 
        {
            _customerRepository = new CustomerRepository();
        }

        public async Task<CustomerDto> AddNewCustomerAsync(string customerName, string contactNo, string emailAddress, string physicalAddress)
        {
            Validate.NullOrEmptyArgument(customerName);
            Validate.NullOrEmptyArgument(contactNo);
            Validate.NullOrEmptyArgument(emailAddress);
            Validate.NullOrEmptyArgument(physicalAddress);

            Validate.ArgumentType(customerName, typeof(string));

            var cus = new CustomerCommand(customerName,contactNo,emailAddress,physicalAddress);

            var customer = await _customerRepository.CreateCustomerAsync(cus);

            return customer !=null ? customer : null;
        }

        public async Task<CustomerDto?> UpdateCustomerDetailsAsync(string customerId, CustomerCommand customer)
        {
            var customerDetailsDto = new CustomerDto
                (Guid.Parse(customerId), customer.CustomerName, customer.ContactNo, customer.EmailAddress, customer.PhysicalAddress);

            return Validate.ValidateCustomerDetailsDto(customerDetailsDto) ? await _customerRepository.UpdateCustomer(customerDetailsDto) : null ;
        }

        public CustomerDto? GetCustomer(Guid id)
        {
            Validate.NullOrEmptyArgument(id.ToString());

            var customer = _customerRepository.GetCustomerById(id.ToString());

            return customer != null ?
                new CustomerDto(id, customer.CustomerName, customer.ContactNo, customer.EmailAddress, customer.PhysicalAddress) : null;
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();

            return customers;
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            Validate.NullOrEmptyArgument(id.ToString());

            var customer = await _customerRepository.DeleteCustomerAsync(id.ToString());

            return customer != false ? customer : false;
        }
    }
}
