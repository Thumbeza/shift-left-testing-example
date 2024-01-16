using We.Sell.Bread.API.Services;

namespace We.Sell.Bread.API.Unit.Tests.Tests.Services
{
    public class CustomerServiceTests
    {
        [Fact]
        public void GivenEmptyIdWhenRetrievingCustomerThrowFormatException()
        {
            var customerService = new CustomerService();
            var emptyId = string.Empty;

            var customer = () => customerService.GetCustomerDetails(new Guid(emptyId));

            customer.Should().Throw<FormatException>().WithMessage("Unrecognized Guid format.");
        }

        [Fact]
        public void GivenEmptyNameWhenAddingNewCustomerThrowFormatException()
        {
            var customerService = new CustomerService();

            var customerName = string.Empty;
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = () => customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().Throw<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenEmptyContactNoWhenAddingNewCustomerThrowFormatException()
        {
            var customerService = new CustomerService();

            var customerName = Faker.Name.FullName();
            var contactNo = string.Empty;
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = () => customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().Throw<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenEmptyEmailWhenAddingNewCustomerThrowFormatException()
        {
            var customerService = new CustomerService();

            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = string.Empty;
            var physicalAddress = Faker.Address.City();

            var customer = () => customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().Throw<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenEmptyAddressWhenAddingNewCustomerThrowFormatException()
        {
            var customerService = new CustomerService();
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = string.Empty;

            var customer = () => customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().Throw<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenCorrectDetailsWhenAddingNewCustomerNewRecordMustBeCreated()
        {
            var customerService = new CustomerService();
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Id.GetType().Should().Be(typeof(Guid));
            customer.CustomerName.Should().Be(customerName);
            customer.ContactNo.Should().Be(contactNo);
            customer.EmailAddress.Should().Be(emailAddress);
            customer.PhysicalAddress.Should().Be(physicalAddress);
        }
    }
}
