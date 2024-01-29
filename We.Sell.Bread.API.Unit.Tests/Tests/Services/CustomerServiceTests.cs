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

            var customer = () => _customerService.GetCustomer(new Guid(emptyId));

            customer.Should().Throw<FormatException>().WithMessage("Unrecognized Guid format.");
        }

        [Fact]
        public void GivenIncorrectIdWhenRetrievingCustomerReturnTypeMustBeNull()
        {
            var customer = _customerService.GetCustomer(CustomerData.IncorrectCustomerIdGuid);

            customer.Should().BeNull();
        }

        [Fact]
        public async Task GivenEmptyNameWhenAddingNewCustomerThrowFormatException()
        {
            var customerName = string.Empty;
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = async () => await _customerService.AddNewCustomerAsync(customerName, contactNo, emailAddress, physicalAddress);

            await customer.Should().ThrowAsync<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public async Task GivenEmptyContactNoWhenAddingNewCustomerThrowFormatException()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = string.Empty;
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = async () => await _customerService.AddNewCustomerAsync(customerName, contactNo, emailAddress, physicalAddress);

            await customer.Should().ThrowAsync<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public async void GivenEmptyEmailWhenAddingNewCustomerThrowFormatException()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = string.Empty;
            var physicalAddress = Faker.Address.City();

            var customer = async () => await _customerService.AddNewCustomerAsync(customerName, contactNo, emailAddress, physicalAddress);

            await customer.Should().ThrowAsync<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public async Task GivenEmptyAddressWhenAddingNewCustomerThrowFormatException()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = string.Empty;

            var customer = async () => await _customerService.AddNewCustomerAsync(customerName, contactNo, emailAddress, physicalAddress);

            await customer.Should().ThrowAsync<ArgumentException>().WithMessage(" cannot be empty or null");
        }

        [Fact]
        public async Task GivenCorrectDetailsWhenAddingNewCustomerNewRecordMustBeCreated()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = await _customerService.AddNewCustomerAsync(customerName, contactNo, emailAddress, physicalAddress);

            customer.Id.GetType().Should().Be(typeof(Guid));
            customer.CustomerName.Should().Be(customerName);
            customer.ContactNo.Should().Be(contactNo);
            customer.EmailAddress.Should().Be(emailAddress);
            customer.PhysicalAddress.Should().Be(physicalAddress);
        }

        [Fact]
        public async Task GivenCorrectDetailsWhenCreatingCustomerReturnTypeMustBeCustomerDetailsDto()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var customer = await _customerService.AddNewCustomerAsync(customerName, contactNo, emailAddress, physicalAddress);

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(CustomerDetailsDto));
        }

        [Fact]
        public void GivenCorrectIdWhenRetrievingCustomerReturnTypeMustBeCustomerDetailsDto()
        {
            var customer = _customerService.GetCustomer(CustomerData.CustomerIdGuid);

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
        public async Task GivenEmptyIdWhenDeletingCustomerMustThrowFormatException()
        {
            var emptyId = string.Empty;

            var customer = async () => await _customerService.DeleteCustomerAsync(new Guid(emptyId));

            await customer.Should().ThrowAsync<FormatException>().WithMessage("Unrecognized Guid format.");
        }

        [Fact]
        public async Task GivenIncorrectIdWhenDeletingCustomerReturnTypeMustBeNull()
        {
            var customer = await _customerService.DeleteCustomerAsync(Guid.NewGuid());

            customer.Should().BeFalse();
        }

        [Fact(Skip ="Under Inverstigation")]
        public async Task GivenCorrectIdWhenDeletingCustomerReturnTypeMustBeBoolionOfTrue()
        {
            var customer = await _customerService.DeleteCustomerAsync(CustomerData.DeleteCustomerIdGuid);

            customer.Should().BeTrue();
        }
    }
}
