using System.Net.Mail;
using System.Net.NetworkInformation;
using We.Sell.Bread.Core.DTOs.Customer;
using We.Sell.Bread.Core.DTOs.Product;
using We.Sell.Bread.Core.Exceptions;

namespace We.Sell.Bread.Core.Validations
{
    public static class Validate
    {
        public static void ArgumentType(string customerName, Type type)
        {
            if (customerName.GetType() != type)
            {
                throw new InvalidTypeException("The parameter did not match the expected type.");
            }
            
        }

        public static void NullOrEmptyArgument(string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be empty or null");
            }
        }

        public static bool ValidateCustomerDetailsDto(CustomerDto customer)
        {
            try
            {
                NullOrEmptyArgument(customer.ContactNo);
                NullOrEmptyArgument(customer.EmailAddress);
                NullOrEmptyArgument(customer.CustomerName);
                NullOrEmptyArgument(customer.PhysicalAddress);
            }
            catch (ArgumentNullException)
            {

                return false;
            }

            return true;
        }

        public static bool ValidateProductDetailsDto(ProductDto product)
        {
            try
            {
                NullOrEmptyArgument(product.ProductName);
                NullOrEmptyArgument(product.Price.ToString());
                NullOrEmptyArgument(product.Description);
                NullOrEmptyArgument(product.StockQuantity.ToString());
            }
            catch (ArgumentNullException)
            {
                return false;
               
            }

            return true;
        }
    }
}
