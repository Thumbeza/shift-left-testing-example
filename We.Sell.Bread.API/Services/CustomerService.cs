using We.Sell.Bread.Core.Interfaces;
using We.Sell.Bread.Core.DTOs.Customer;
using We.Sell.Bread.Core.Validations;

namespace We.Sell.Bread.API.Services
{
    public class CustomerService : ICustomerService
    {
        public CustomerDetailsDto AddNewCustomer(string customerName, string contactNo, string emailAddress, string physicalAddress)
        {
            Validate.NullOrEmptyArgument(customerName);
            Validate.NullOrEmptyArgument(contactNo);
            Validate.NullOrEmptyArgument(emailAddress);
            Validate.NullOrEmptyArgument(physicalAddress);

            Validate.ArgumentType(customerName, typeof(string));

            var id = Guid.NewGuid();

            return new CustomerDetailsDto(id, customerName, contactNo, emailAddress, physicalAddress);
        }

        public CustomerDetailsDto GetCustomerDetails(Guid id)
        {
            //Fake customer Details
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            return new CustomerDetailsDto(id, customerName, contactNo, emailAddress, physicalAddress);
        }

        public IEnumerable<CustomerDetailsDto> GetCustomers()
        {
            throw new NotImplementedException();
        }
    }
}
