using We.Sell.Bread.Core.DTOs.Customer;
using We.Sell.Bread.Core.Interfaces;
using We.Sell.Bread.Infrastructure.Helpers;

namespace We.Sell.Bread.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private string _basePath;
        private string _customerFilePath;

        public CustomerRepository() 
        {
            _basePath = FileHelper.GetBasePath();

            _customerFilePath = $"{_basePath}/We.Sell.Bread.Infrastructure/DataFiles/Customer.json";
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerCommand entity)
        {
           var customerJson = JsonHelper.ReadJsonFile(_customerFilePath);

           var customers = JsonHelper.Deserialize<List<CustomerDto>>(customerJson);

            var newCustomer = new CustomerDto(new Guid(), entity.CustomerName, entity.ContactNo, entity.EmailAddress, entity.PhysicalAddress)
            {
                Id = Guid.NewGuid(),
                CustomerName = entity.CustomerName,
                ContactNo = entity.ContactNo,
                EmailAddress = entity.EmailAddress,
                PhysicalAddress = entity.PhysicalAddress,
            };
             
            customers.Add(newCustomer);


            await JsonHelper.StreamWriteAsync(customers, _customerFilePath);

            return newCustomer;
        }

        public IEnumerable<CustomerDto> GetAllCustomers()
        {
            var customerJson = JsonHelper.ReadJsonFile(_customerFilePath);

            var customers = JsonHelper.Deserialize<IEnumerable<CustomerDto>>(customerJson);

            return customers;
        }

        public CustomerDto? GetCustomerById(string customerId)
        {
            var customerJson = JsonHelper.ReadJsonFile(_customerFilePath);

            var customers = JsonHelper.Deserialize<IEnumerable<CustomerDto>>(customerJson);

            var customer = customers.FirstOrDefault(fruit => fruit.Id.ToString() == customerId);

            return customer;
        }

        public async Task<CustomerDto> UpdateCustomer(CustomerDto entity)
        {
            var customerJsonString = JsonHelper.ReadJsonFile(_customerFilePath);

            var existingCustomersList = JsonHelper.Deserialize<IList<CustomerDto>>(customerJsonString);

            var customerDtoToFind = existingCustomersList.FirstOrDefault(x => x.Id.ToString() == entity.Id.ToString());

            existingCustomersList.Remove(customerDtoToFind);
            existingCustomersList.Add(entity);

            await JsonHelper.StreamWriteAsync(existingCustomersList, _customerFilePath);

            return entity;
        }

        public async Task<bool> DeleteCustomerAsync(string customerId)
        {
            var customerJson = JsonHelper.ReadJsonFile(_customerFilePath);

            var customers = JsonHelper.Deserialize<List<CustomerDto>>(customerJson);

            var customerToRemove = customers.FirstOrDefault(Cus => Cus.Id.ToString() == customerId);

            if(customerToRemove != null)
            {
                customers.Remove(customerToRemove);

               await JsonHelper.StreamWriteAsync(customers, _customerFilePath);

                return true;
            }
            else { return false; }
        }
    }
}
