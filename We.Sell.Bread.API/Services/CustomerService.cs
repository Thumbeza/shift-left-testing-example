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

        public async Task<CustomerDetailsDto> AddNewCustomerAsync(string customerName, string contactNo, string emailAddress, string physicalAddress)
        {
            Validate.NullOrEmptyArgument(customerName);
            Validate.NullOrEmptyArgument(contactNo);
            Validate.NullOrEmptyArgument(emailAddress);
            Validate.NullOrEmptyArgument(physicalAddress);

            Validate.ArgumentType(customerName, typeof(string));

            var cus = new NewCustomerDto(customerName,contactNo,emailAddress,physicalAddress);

            var customer = await _customerRepository.CreateCustomerAsync(cus);

            return customer !=null ? customer : null;
        }

        public CustomerDetailsDto? GetCustomer(Guid id)
        {
            Validate.NullOrEmptyArgument(id.ToString());

            var customer = _customerRepository.GetCustomerById(id.ToString());

            return customer != null ?
                new CustomerDetailsDto(id, customer.CustomerName, customer.CustomerName, customer.EmailAddress, customer.PhysicalAddress) : null;
        }

        public IEnumerable<CustomerDetailsDto> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            Validate.NullOrEmptyArgument(id.ToString());

            var customer = await _customerRepository.DeleteCustomerAsync(id.ToString());

            return customer != false ? customer : false;
        }
    }
}
