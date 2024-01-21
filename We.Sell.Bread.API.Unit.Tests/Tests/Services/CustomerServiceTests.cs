using We.Sell.Bread.API.Unit.Tests.TestData;

namespace We.Sell.Bread.API.Unit.Tests.Tests.Services
{
    public class CustomerServiceTests
    {
        private CustomerService _customerService;

        public CustomerServiceTests() 
        { 
            _customerService = new CustomerService();
        }

        [Fact]
        public void GivenEmptyIdWhenRetrievingCustomerThrowFormatException()
        {
            var emptyId = string.Empty;

            var customer = () => _customerService.GetCustomerDetails(new Guid(emptyId));

            customer.Should().Throw<FormatException>().WithMessage("Unrecognized Guid format.");
        }

        [Fact]
        public void GivenIncorrectIdWhenRetrievingCustomerReturnTypeMustBeNull()
        {
            var customer = _customerService.GetCustomerDetails(CustomerData.IncorrectCustomerIdGuid);

            customer.Should().BeNull();
        }

        [Fact]
        public void GivenEmptyNameWhenAddingNewCustomerThrowFormatException()
        {
            var customerName = string.Empty;
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = () => _customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().Throw<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenEmptyContactNoWhenAddingNewCustomerThrowFormatException()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = string.Empty;
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = () => _customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().Throw<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenEmptyEmailWhenAddingNewCustomerThrowFormatException()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = string.Empty;
            var physicalAddress = Faker.Address.City();

            var customer = () => _customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().Throw<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenEmptyAddressWhenAddingNewCustomerThrowFormatException()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = string.Empty;

            var customer = () => _customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().Throw<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public void GivenCorrectDetailsWhenAddingNewCustomerNewRecordMustBeCreated()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = _customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Id.GetType().Should().Be(typeof(Guid));
            customer.CustomerName.Should().Be(customerName);
            customer.ContactNo.Should().Be(contactNo);
            customer.EmailAddress.Should().Be(emailAddress);
            customer.PhysicalAddress.Should().Be(physicalAddress);
        }

        [Fact]
        public void GivenCorrectDetailsWhenCreatingCustomerReturnTypeMustBeCustomerDetailsDto()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = _customerService.AddNewCustomer(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(CustomerDetailsDto));
        }

        [Fact]
        public void GivenCorrectIdWhenRetrievingCustomerReturnTypeMustBeCustomerDetailsDto()
        {
            var customer = _customerService.GetCustomerDetails(CustomerData.CustomerIdGuid);

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(CustomerDetailsDto));
        }

        [Fact]
        public void GivenDataExistWhenRetrievingCustomerReturnTypeMustBeListOfCustomerDetailsDto()
        {
            var customer = _customerService.GetAllCustomers();

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(List<CustomerDetailsDto>));
        }

        [Fact]
        public void GivenEmptyIdWhenDeletingCustomerMustThrowFormatException()
        {
            var emptyId = string.Empty;

            var customer = () => _customerService.DeleteCustomerDetails(new Guid(emptyId));

            customer.Should().Throw<FormatException>().WithMessage("Unrecognized Guid format.");
        }

        [Fact]
        public void GivenIncorrectIdWhenDeletingCustomerReturnTypeMustBeNull()
        {
            var customer = _customerService.DeleteCustomerDetails(Guid.NewGuid());

            customer.Should().BeFalse();
        }

        [Fact]
        public void GivenCorrectIdWhenDeletingCustomerReturnTypeMustBeBoolionOfTrue()
        {
            var customer = _customerService.DeleteCustomerDetails(CustomerData.DeleteCustomerIdGuid);

            customer.Should().BeTrue();
        }
    }
}
