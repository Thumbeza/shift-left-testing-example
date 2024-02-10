using Castle.Core.Resource;
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

            await _customerService.DeleteCustomerAsync(customer.Id);
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
            customer.Should().BeOfType(typeof(CustomerDto));

            await _customerService.DeleteCustomerAsync(customer.Id);
        }

        [Fact]
        public void GivenCorrectIdWhenRetrievingCustomerReturnTypeMustBeCustomerDetailsDto()
        {
            var customer = _customerService.GetCustomer(CustomerData.CustomerIdGuid);

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(CustomerDto));
        }

        [Fact]
        public void GivenDataExistWhenRetrievingCustomerReturnTypeMustBeListOfCustomerDetailsDto()
        {
            var customer = _customerService.GetAllCustomers();

            customer.Should().NotBeNull();
            customer.Should().BeOfType(typeof(List<CustomerDto>));
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

        [Fact(Skip = "Awaiting bug #24 to be resolved")]
        public async void GivenDifferentCustomerDetailWhenUpdatingCustomerDetailsMustBeChanged()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var testCustomer = await _customerService.AddNewCustomerAsync(customerName, contactNo, emailAddress, physicalAddress);
            
            var testCustomerId = testCustomer.Id;
            var updatedName = "Customer Service Tests";
            var newCustomerDto = new CustomerCommand(updatedName, contactNo, emailAddress, physicalAddress);

            await _customerService.UpdateCustomerDetailsAsync(testCustomerId.ToString(), newCustomerDto);
            
            var existingCustomersRecord = _customerService.GetCustomer(testCustomerId);

            existingCustomersRecord.Should().NotBeNull();
            existingCustomersRecord.CustomerName.Should().Be(updatedName);
            existingCustomersRecord.CustomerName.Should().NotBe(testCustomer.CustomerName);
            existingCustomersRecord.ContactNo.Should().Be(testCustomer.ContactNo);
            existingCustomersRecord.EmailAddress.Should().Be(testCustomer.EmailAddress);
            existingCustomersRecord.PhysicalAddress.Should().Be(testCustomer.PhysicalAddress);
            
            await _customerService.DeleteCustomerAsync(testCustomer.Id);
        }

        [Fact(Skip = "Awaiting bug #24 to be resolved")]
        public async void GivenCorrectDetailsWhenUpdatingCustomerReturnTypeMustBeCustomerDetailsDto()
        {
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var testCustomer = await _customerService.AddNewCustomerAsync(customerName, contactNo, emailAddress, physicalAddress);
            
            var customerId = testCustomer.Id.ToString();
            var testCustomerUpdatedName = "Test Customer Update";
            var newCustomerDetailsDto = new CustomerCommand(testCustomerUpdatedName, contactNo, emailAddress, physicalAddress);
            
            var updatedCustomer = await _customerService.UpdateCustomerDetailsAsync(customerId, newCustomerDetailsDto);

            updatedCustomer.Should().NotBeNull();
            updatedCustomer.Should().BeOfType(typeof(CustomerDto));
            
            await _customerService.DeleteCustomerAsync(testCustomer.Id);
        }
        
        [Fact]
        public async Task GivenEmptyNameWhenUpdatingCustomerReturnShouldBeNull()
        {
            var customerId = Guid.NewGuid().ToString();
            var customerName = string.Empty;
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var newCustomerDto = new CustomerCommand(customerName, contactNo, emailAddress, physicalAddress);

            var customer = await _customerService.UpdateCustomerDetailsAsync(customerId, newCustomerDto);

            customer.Should().BeNull();
        }

        [Fact]
        public async Task GivenEmptyContactNoWhenUpdatingCustomerReturnShouldBeNull()
        {
            var customerId = Guid.NewGuid().ToString();
            var customerName = Faker.Name.FullName();
            var contactNo = string.Empty;
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = Faker.Address.City();

            var newCustomerDto = new CustomerCommand(customerName, contactNo, emailAddress, physicalAddress);

            var customer = await _customerService.UpdateCustomerDetailsAsync(customerId, newCustomerDto);

            customer.Should().BeNull();
        }

        [Fact]
        public async void GivenEmptyEmailWhenUpdatingCustomerReturnShouldBeNull()
        {
            var customerId = Guid.NewGuid().ToString();
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = string.Empty;
            var physicalAddress = Faker.Address.City();

            var newCustomerDto = new CustomerCommand(customerName, contactNo, emailAddress, physicalAddress);

            var customer = await _customerService.UpdateCustomerDetailsAsync(customerId, newCustomerDto);

            customer.Should().BeNull();
        }

        [Fact]
        public async Task GivenEmptyAddressWhenUpdatingCustomerReturnShouldBeNull()
        {
            var customerId = Guid.NewGuid().ToString();
            var customerName = Faker.Name.FullName();
            var contactNo = Faker.Phone.Number();
            var emailAddress = Faker.Internet.Email();
            var physicalAddress = string.Empty;

            var newCustomerDto = new CustomerCommand(customerName, contactNo, emailAddress, physicalAddress);

            var customer = await _customerService.UpdateCustomerDetailsAsync(customerId, newCustomerDto);

            customer.Should().BeNull();
        }
    }
}
