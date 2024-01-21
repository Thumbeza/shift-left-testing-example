using Newtonsoft.Json;
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

            _customerFilePath = $"{_basePath}\\We.Sell.Bread.Infrastructure\\DataFiles\\Customer.json"; ;
        }

        public CustomerDetailsDto CreateCustomer(NewCustomerDto entity)
        {
            var customerJson = JsonHelper.ReadJsonFile(_customerFilePath);

            var customers = JsonHelper.Deserialize<List<CustomerDetailsDto>>(customerJson);

            var newCustomer = new CustomerDetailsDto(new Guid(), entity.CustomerName, entity.ContactNo, entity.EmailAddress, entity.PhysicalAddress)
            {
                Id = Guid.NewGuid(),
                CustomerName = entity.CustomerName,
                ContactNo = entity.ContactNo,
                EmailAddress = entity.EmailAddress,
                PhysicalAddress = entity.PhysicalAddress,
            };
             
            customers.Add(newCustomer);

            var updatedJsonContent = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText(_customerFilePath, updatedJsonContent);

            return newCustomer;
        }

        public IEnumerable<CustomerDetailsDto> GetAllCustomers()
        {
            var customerJson = JsonHelper.ReadJsonFile(_customerFilePath);

            var customers = JsonHelper.Deserialize<IEnumerable<CustomerDetailsDto>>(customerJson);

            return customers;
        }

        public CustomerDetailsDto? GetCustomerById(string customerId)
        {
            var customerJson = JsonHelper.ReadJsonFile(_customerFilePath);

            var customers = JsonHelper.Deserialize<IEnumerable<CustomerDetailsDto>>(customerJson);

            var customer = customers.FirstOrDefault(fruit => fruit.Id.ToString() == customerId);

            return customer;
        }

        public CustomerDetailsDto UpdateCustomer(string customerId, NewCustomerDto entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCustomer(string customerId)
        {
            var customerJson = JsonHelper.ReadJsonFile(_customerFilePath);

            var customers = JsonHelper.Deserialize<List<CustomerDetailsDto>>(customerJson);

            var customerToRemove = customers.FirstOrDefault(Cus => Cus.Id.ToString() == customerId);

            if(customerToRemove != null)
            {
                customers.Remove(customerToRemove);

                var updatedJsonContent = JsonConvert.SerializeObject(customers, Formatting.Indented);
                File.WriteAllText(_customerFilePath, updatedJsonContent);

                return true;
            }
            else { return false; }
        }
    }
}
