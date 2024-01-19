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

        public CustomerDetailsDto AddNewCustomer(string customerName, string contactNo, string emailAddress, string physicalAddress)
        {
            Validate.NullOrEmptyArgument(customerName);
            Validate.NullOrEmptyArgument(contactNo);
            Validate.NullOrEmptyArgument(emailAddress);
            Validate.NullOrEmptyArgument(physicalAddress);

            Validate.ArgumentType(customerName, typeof(string));

            var id = Guid.NewGuid();

            //write to file

            return new CustomerDetailsDto(id, customerName, contactNo, emailAddress, physicalAddress);
        }

        public CustomerDetailsDto? GetCustomerDetails(Guid id)
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

        public bool DeleteCustomerDetails(Guid id)
        {
            // This is a placeholder; you would typically interact with a database or data source.

            return true;
        }
    }
}
