using System.Reflection;
using We.Sell.Bread.Core.DTOs.Customer;
using We.Sell.Bread.Core.Interfaces;
using We.Sell.Bread.Infrastructure.Helpers;

namespace We.Sell.Bread.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        //need to change tp relative path
        private string _customerFilePath = Path.GetFullPath("DataFiles/Customer.json");
        //"C:\\Users\\kwanele.nzimande\\Documents\\POC\\shift-left-testing-example\\We.Sell.Bread.Infrastructure\\DataFiles\\Customer.json";

        public CustomerDetailsDto CreateCustomer(NewCustomerDto entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
